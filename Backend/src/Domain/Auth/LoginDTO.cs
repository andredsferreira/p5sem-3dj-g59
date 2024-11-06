using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domain.Auth;

public class LoginDTO {

    [Required]
    [FromForm(Name = "username")]
    public string Username { get; set; }

    [Required]
    [FromForm(Name = "password")]
    public string Password { get; set; }

}