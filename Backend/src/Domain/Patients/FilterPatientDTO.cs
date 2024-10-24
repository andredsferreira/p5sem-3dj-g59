using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class FilterPatientDTO
{

    public Guid id { get; set; } = Guid.NewGuid();

    public string? MedicalRecordNumber {get;set;}
    public DateOnly? DateOfBirth { get; set;}
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender {get;set;}
    public string? FullName { get; set; }
    public string? Allergies { get; set; }
}
