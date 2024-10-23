using System;
using System.Collections.Generic;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class Patient : Entity<PatientId>, IAggregateRoot {

    public MedicalRecordNumber MedicalRecordNumber {get; protected set;}
    public DateOnly DateOfBirth { get; protected set; }

    public string Email { get; protected set; }

    public string PhoneNumber { get; protected set; }
    public Gender Gender {get; protected set;}

    public FullName FullName { get; protected set; }

    public List<string> Allergies { get; protected set; }

    public ICollection<OperationRequest> OperationRequests { get; set; } = [];

    private Patient() {

    }

    public Patient(DateOnly DateOfBirth, string Email, string PhoneNumber, Gender Gender, FullName FullName, List<string> Allergies) {
        Id = new PatientId(Guid.NewGuid());
        MedicalRecordNumber = null;
        this.DateOfBirth = DateOfBirth;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Gender = Gender;
        this.FullName = FullName;
        this.Allergies = Allergies;
    }

    public static Patient createFromDTO(PatientDTO dto) {
        return new Patient(dto.DateOfBirth, dto.Email, dto.PhoneNumber, dto.Gender, new FullName(dto.FullName), [.. dto.Allergies.Split(", ")]);
    }
    public PatientDTO returnDTO() {
        return new PatientDTO(DateOfBirth, Email, PhoneNumber, Gender, FullName.ToString(), Allergies.ToString());
    }
}
