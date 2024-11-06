using System.ComponentModel.DataAnnotations;

namespace Backend.Domain;

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