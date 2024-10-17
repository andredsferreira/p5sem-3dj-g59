using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.Auth;

public class UserDTO {

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public int Role { get; set; }
    
}