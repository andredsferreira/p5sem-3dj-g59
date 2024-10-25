using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.Auth;

public class LoginDTO {

    [Required]
    [FromForm(Name = "username")]
    public string Username { get; set; }

    [Required]
    [FromForm(Name = "password")]
    public string Password { get; set; }

}