using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Patients;

public class FilterPatientDTO {

    public string MedicalRecordNumber { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    [RegularExpression("^(Male|Female|Others)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Others'.")]
    public string Gender { get; set; }

    public string FullName { get; set; }

    public string Allergies { get; set; }
    
}
