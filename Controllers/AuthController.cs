using DDDSample1.Auth;
using DDDSample1.Domain.Auth;
using DDDSample1.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase {

    private readonly IConfiguration configuration;

    private readonly ILogger<AuthController> logger;

    private readonly IdentityContext context;

    private readonly UserManager<IdentityUser> userManager;

    private readonly SignInManager<IdentityUser> signInManager;

    public AuthController(IConfiguration configuration, ILogger<AuthController> logger, IdentityContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
        this.configuration = configuration;
        this.logger = logger;
        this.context = context;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterDTO dto) {
        try {
            if (!ModelState.IsValid) {
                var details = new ValidationProblemDetails(ModelState);
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }
            var newUser = new IdentityUser();
            newUser.UserName = dto.UserName;
            newUser.Email = dto.Email;
            var result = await userManager.CreateAsync(newUser, dto.Password);
            if (result.Succeeded) {
                logger.LogInformation("User {userName} ({email}) has been created.", newUser.UserName, newUser.Email);
                return StatusCode(201, $"User '{newUser.UserName}' has been created.");
            }
            else {
                throw new Exception(string.Format("Error: {0}", string.Join(" ", result.Errors.Select(e => e.Description))));
            }
        }
        catch (Exception e) {
            var exceptionDetails = new ProblemDetails();
            exceptionDetails.Detail = e.Message;
            exceptionDetails.Status = StatusCodes.Status500InternalServerError;
            return StatusCode(StatusCodes.Status500InternalServerError, exceptionDetails);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginDTO dto) {
        try {
            if (!ModelState.IsValid) {
                var details = new ValidationProblemDetails(ModelState);
                details.Status = StatusCodes.Status400BadRequest;
                return new BadRequestObjectResult(details);
            }

            var user = await userManager.FindByNameAsync(dto.Username);

            if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password)) {
                throw new Exception("Invalid login attempt.");
            }
            else {
                var jwtSettings = configuration.GetSection("Jwt");
                var keyEncoding = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
                var key = new SymmetricSecurityKey(keyEncoding);
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));

                var jwtObject = new JwtSecurityToken(issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims, expires: DateTime.Now.AddSeconds(540),
                    signingCredentials: signingCredentials);

                var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

                return StatusCode(StatusCodes.Status200OK, jwtString);
            }
        }
        catch (Exception e) {
            var exceptionDetails = new ProblemDetails();
            exceptionDetails.Detail = e.Message;
            exceptionDetails.Status = StatusCodes.Status401Unauthorized;
            return StatusCode(StatusCodes.Status401Unauthorized, exceptionDetails);
        }
    }

}