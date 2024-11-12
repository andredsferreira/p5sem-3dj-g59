using Backend.Domain;
using Backend.Domain.Auth;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Infrastructure;
using Backend.Infrastructure.Shared.MessageSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {

    private readonly IConfiguration Configuration;

    private readonly AppDbContext Context;

    private readonly UserManager<IdentityUser> UserManager;

    private readonly SignInManager<IdentityUser> SignInManager;

    private readonly IPatientRepository _patientRepository;

    private readonly IMessageSenderService MessageSender;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUnitOfWork _unitOfWork;

    public AuthController(IConfiguration Configuration, AppDbContext context, UserManager<IdentityUser> UserManager,
                        SignInManager<IdentityUser> SignInManager, IPatientRepository patientRepository,
                        IMessageSenderService MessageSender, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) {
        this.Configuration = Configuration;
        this.Context = context;
        this.UserManager = UserManager;
        this.SignInManager = SignInManager;
        this._patientRepository = patientRepository;
        this.MessageSender = MessageSender;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDTO dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        var result = await SignInManager.PasswordSignInAsync(dto.Username, dto.Password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded) {
            return Unauthorized("Invalid login attempt.");
        }
        var user = await UserManager.FindByNameAsync(dto.Username);
        var token = await BuildToken(user);
        return Ok(new { token });
    }

    [HttpPost("registerbackoffice")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<IActionResult> RegisterBackoffice([FromBody] RegisterBackofficeDTO dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }
        if (dto.Role == HospitalRoles.Patient)
            return BadRequest(dto.Role);

        var backofficeUser = new IdentityUser {
            UserName = dto.Username,
            Email = dto.Email,
            PhoneNumber = dto.Phone
        };
        var result = await UserManager.CreateAsync(backofficeUser, dto.Password);
        if (!result.Succeeded) {
            return BadRequest(result.Errors);
        }

        var userRoles = await UserManager.GetRolesAsync(backofficeUser);
        if (userRoles.Count > 0) {
            return BadRequest("User already has a role assigned.");
        }

        await UserManager.AddToRoleAsync(backofficeUser, dto.Role);

        var token = await BuildToken(backofficeUser);

        SendEmailConfirmationEmail(backofficeUser, token);

        return Ok(new { token });
    }

    [HttpPost("registerpatient")]
    public async Task<IActionResult> RegisterPatient([FromBody]
    PatientRegisterDTO dto) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var patientUser = new IdentityUser {
            UserName = dto.Username,
            Email = dto.Email,
            PhoneNumber = dto.Phone
        };
        var pat = _patientRepository.GetByEmail(new MailAddress(dto.Email));
        if (pat == null) {
            return NotFound("Patient is not registered by the admin");
        }
        var existingUser = await UserManager.FindByNameAsync(dto.Username);
        if (existingUser != null) {
            return BadRequest("Username is already taken");
        }

        pat.LinkToAccount(dto.Email);
        _patientRepository.Update(pat);
        var result = await UserManager.CreateAsync(patientUser, dto.Password);
        if (!result.Succeeded) {
            return BadRequest(result.Errors);
        }

        var userRoles = await UserManager.GetRolesAsync(patientUser);
        if (userRoles.Count > 0) {
            return BadRequest("User already has a role assigned.");
        }

        await UserManager.AddToRoleAsync(patientUser, HospitalRoles.Patient);

        var token = await BuildToken(patientUser);

        await _unitOfWork.CommitAsync();

        return Ok(new { token });
    }

    [HttpDelete("DeleteProfile")]
    [Authorize(Roles = HospitalRoles.Patient)]
    public IActionResult DeletePatientProfile() {
        MailAddress email = new(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value);
        Patient pat = CheckCurrentUsersPatientProfile(email);
        //Ok
        SendAccountDeletionEmail(email, pat);
        return Ok(pat.returnDTO());
    }
    private Patient CheckCurrentUsersPatientProfile(MailAddress email) {
        Patient pat = _patientRepository.GetByUserEmail(email);
        Console.WriteLine("Este é o patient: " + pat);
        return pat;
    }
    private void SendAccountDeletionEmail(MailAddress recipient, Patient pat) {
        string appDomain = Configuration.GetSection("Application:AppDomain").Value,
            confirmationLink = Configuration.GetSection("Application:EmailDeletionConfirmation").Value,
            fullConfirmationLink = string.Format(appDomain + confirmationLink, pat.Id);

        string emailBody = string.Format(
            "Hello {0},<br><br>" +
            "Please <a href=\"{1}\">confirm your account's deletion</a> by clicking the link.<br><br>" +
            "We're sad to see you go...",
            pat.FullName.ToString(), fullConfirmationLink
        );

        MessageSender.SendMessage(recipient.ToString(), "Account Deletion", emailBody);
    }

    [HttpDelete("confirmation-deletion-email")]
    [Authorize(Roles = HospitalRoles.Patient)]
    public async Task<IActionResult> DeletePatientProfileAndRecordsAsync() {
        string email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        await UserManager.DeleteAsync(await UserManager.FindByEmailAsync(email));
        var pat = _patientRepository.GetByUserEmail(new MailAddress(email));
        _patientRepository.Remove(pat);
        await _unitOfWork.CommitAsync();
        return Ok(pat.returnDTO());
    }

    private async Task<string> BuildToken(IdentityUser user) {
        var userRoles = await UserManager.GetRolesAsync(user);
        var role = userRoles.FirstOrDefault();

        if (user == null) {
            throw new ArgumentException("User cannot be null.");
        }

        if (role == null) {
            throw new ArgumentException("User does not have an assigned role.");
        }

        var claims = new List<Claim>() {
            new Claim(HospitalClaims.Id, user.Id),
            new Claim(HospitalClaims.Username, user.UserName),
            new Claim(HospitalClaims.Email, user.Email),
            new Claim(HospitalClaims.Role, role),
        };

        var jwtSettings = Configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(24);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpGet("confirmation-email")]
    public async Task ConfirmEmail(string uid, string token) {
        if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token)) {
            token = token.Replace(' ', '+');
            await UserManager.ConfirmEmailAsync(await UserManager.FindByIdAsync(uid), token);
        }
    }

    private void SendEmailConfirmationEmail(IdentityUser user, string token) {
        string appDomain = Configuration.GetSection("Application:AppDomain").Value,
            confirmationLink = Configuration.GetSection("Application:EmailConfirmation").Value,
            fullConfirmationLink = string.Format(appDomain + confirmationLink, user.Id, token);

        string emailBody = string.Format(
            "Hello {0},<br><br>" +
            "Please <a href=\"{1}\">verify your account</a> by clicking the link.<br><br>" +
            "Thank you!",
            user.UserName, fullConfirmationLink
        );

        MessageSender.SendMessage(user.Email, "Account Activation", emailBody);
    }

    [HttpGet("users")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers() {
        var users = await Context.Users.ToListAsync();
        return users.Any() ? Ok(users) : NotFound();
    }
}
