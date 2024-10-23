using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.Auth;

public class LoginDTO {

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

}