using DDDSample1.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {

    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration) {
        _configuration = configuration;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginDTO request) {
        if (ModelState.IsValid) {
            var user = UserStore.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null) {
                return Unauthorized("Invalid user credentials.");
            }
            var token = IssueToken(user);
            return Ok(new { Token = token });
        }
        return BadRequest("Invalid Request Body");
    }

    private string IssueToken(User user) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Defines a set of claims to be included in the token.
        var claims = new List<Claim> {
                new Claim("Hospital_User_Id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

        user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

        // Creates a new JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1), 
            signingCredentials: credentials);

        // Serializes the JWT token to a string and returns it.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}