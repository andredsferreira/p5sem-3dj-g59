using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class FilterPatientDTO
{
    public string? MedicalRecordNumber {get;set;}
    public DateOnly? DateOfBirth { get; set;}
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    [RegularExpression("^(Male|Female|Others)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Others'.")]
    public string? Gender {get;set;}
    public string? FullName { get; set; }
    public string? Allergies { get; set; }
}
