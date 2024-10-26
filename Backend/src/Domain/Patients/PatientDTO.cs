using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class PatientDTO(string MedicalRecordNumber, DateOnly DateOfBirth, string Email, string PhoneNumber, string Gender, string FullName, string Allergies) {

    //public Guid id { get; set; } = Guid.NewGuid();
    public string MedicalRecordNumber { get; set; } = MedicalRecordNumber;

    [Required]
    public DateOnly DateOfBirth { get; private set; } = DateOfBirth;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = Email;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = PhoneNumber;
    [Required]
    [RegularExpression("^(Male|Female|Others)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Others'.")]
    public string Gender { get; set; } = Gender;

    [Required]
    public string FullName { get; set; } = FullName;

    public string Allergies { get; set; } = Allergies;
    
}
