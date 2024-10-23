using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.Auth;


public class PatientRegisterDTO {

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