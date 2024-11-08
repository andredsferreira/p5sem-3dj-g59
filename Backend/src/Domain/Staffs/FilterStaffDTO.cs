using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Staffs;

public class FilterStaffDTO {

    public string LicenseNumber { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    public string FullName { get; set; }

    
}
