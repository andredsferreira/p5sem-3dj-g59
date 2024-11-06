using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Auth;

namespace DDDSample1.Domain;

public class RegisterBackofficeDTO {
    
    [Required]
    public string Role { get; set; }
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }

    [Required]
    public string Password { get; set; }
}