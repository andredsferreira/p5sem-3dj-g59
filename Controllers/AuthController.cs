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
    // Private field to hold the configuration settings injected through the constructor.
    private readonly IConfiguration _configuration;

    // Constructor that accepts IConfiguration and initializes the _configuration field.
    public AuthController(IConfiguration configuration) {
        _configuration = configuration;
    }

    // Action method responding to POST requests on "api/Users/Login".
    // Expects a Login model in the request body.
    [HttpPost("Login")]
    public IActionResult Login([FromBody] Login request) {
        // Checks if the provided model (request) is valid based on data annotations in the Login model.
        if (ModelState.IsValid) {
            // Searches for a user in a predefined user store that matches both username and password.
            var user = UserStore.Users.FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            // Checks if the user object is null, which means no matching user was found.
            if (user == null) {
                // Returns a 401 Unauthorized response with a custom message.
                return Unauthorized("Invalid user credentials.");
            }

            // Calls a method to generate a JWT token for the authenticated user.
            var token = IssueToken(user);

            // Returns a 200 OK response, encapsulating the JWT token in an anonymous object.
            return Ok(new { Token = token });
        }

        // If the model state is not valid, returns a 400 Bad Request response with a custom message.
        return BadRequest("Invalid Request Body");
    }

    // Private method to generate a JWT token using the user's data.
    private string IssueToken(User user) {
        // Creates a new symmetric security key from the JWT key specified in the app configuration.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        // Sets up the signing credentials using the above security key and specifying the HMAC SHA256 algorithm.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Defines a set of claims to be included in the token.
        var claims = new List<Claim>
        {
                // Custom claim using the user's ID.
                new Claim("Myapp_User_Id", user.Id.ToString()),
                // Standard claim for user identifier, using username.
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                // Standard claim for user's email.
                new Claim(ClaimTypes.Email, user.Email),
                // Standard JWT claim for subject, using user ID.
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

        // Adds a role claim for each role associated with the user.
        user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

        // Creates a new JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1), // Token expiration set to 1 hour from the current time.
            signingCredentials: credentials);

        // Serializes the JWT token to a string and returns it.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}