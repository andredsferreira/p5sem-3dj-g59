using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class PatientDTO(DateOnly DateOfBirth, string Email, string PhoneNumber, string FullName, string Allergies)
{

    public Guid id { get; set; } = Guid.NewGuid();

    [Required]
    public DateOnly DateOfBirth { get; private set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Allergies { get; set; }
}
