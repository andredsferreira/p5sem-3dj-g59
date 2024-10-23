using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class PatientDTO(MedicalRecordNumber MedicalRecordNumber, DateOnly DateOfBirth, string Email, string PhoneNumber, Gender Gender, string FullName, string Allergies)
{

    public Guid id { get; set; } = Guid.NewGuid();
    public MedicalRecordNumber MedicalRecordNumber {get;set;} = MedicalRecordNumber;

    [Required]
    public DateOnly DateOfBirth { get; private set; } = DateOfBirth;

    [Required]
    public string Email { get; set; } = Email;

    [Required]
    public string PhoneNumber { get; set; } = PhoneNumber;
    [Required]
    public Gender Gender {get;set;} = Gender;

    [Required]
    public string FullName { get; set; } = FullName;

    public string Allergies { get; set; } = Allergies;
}
