using DDDSample1.Domain;
using DDDSample1.Domain.Auth;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared.MessageSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {

    private readonly IConfiguration Configuration;

    private readonly IdentityContext Context;

    private readonly UserManager<IdentityUser> UserManager;

    private readonly SignInManager<IdentityUser> SignInManager;

    private readonly IMessageSenderService MessageSender;

    public AuthController(IConfiguration Configuration, IdentityContext Context, UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager, IMessageSenderService MessageSender) {
        this.Configuration = Configuration;
        this.Context = Context;
        this.UserManager = UserManager;
        this.SignInManager = SignInManager;
        this.MessageSender = MessageSender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromForm] LoginDTO dto) {
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
        RoleType role = (RoleType)Enum.Parse(typeof(RoleType), dto.Role);
        if (role == RoleType.Patient)
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

        return Ok(new { token });
    }

    private async Task<string> BuildToken(IdentityUser user) {
        // Obter role do user
        var userRoles = await UserManager.GetRolesAsync(user);
        var firstRole = userRoles.FirstOrDefault();

        if (user == null) {
            throw new ArgumentException("User cannot be null.");
        }

        if (firstRole == null) {
            throw new ArgumentException("User does not have an assigned role.");
        }

        var claims = new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, firstRole)
        };

        var jwtSettings = Configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(1);


        JwtSecurityToken token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
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
