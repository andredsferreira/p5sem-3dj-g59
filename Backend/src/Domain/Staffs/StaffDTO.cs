using System;
using System.ComponentModel.DataAnnotations;


namespace Backend.Domain.Staffs;

public class StaffDTO(string StaffRole, string IdentityUsername, string email, string phone,string name, string licenseNumber) {

    public string StaffRole { get; set; } = StaffRole;
    public string IdentityUsername { get; set; } = IdentityUsername;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = email;
    [Required]
    [Phone]
    public string Phone { get; set; } = phone;
    [Required]
    public string Name { get; set; } = name;
    [Required]
    public string LicenseNumber { get; set; } = licenseNumber;
}

