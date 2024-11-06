using System;
using System.ComponentModel.DataAnnotations;


namespace Backend.Domain.Staffs;

public class StaffDTO(string StaffRole, string IdentityUsername, string name, string email, string phone, string licenseNumber) {

    public Guid id { get; set; } = Guid.NewGuid();

    public string StaffRole { get; set; } = StaffRole;
    public string IdentityUsername { get; set; } = IdentityUsername;
    [Required]
    public string Name { get; set; } = name;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = email;
    [Required]
    [Phone]
    public string Phone { get; set; } = phone;
    public string LicenseNumber { get; set; } = licenseNumber;

}

