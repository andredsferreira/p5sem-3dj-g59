using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients;

public class Patient : Entity<PatientId>, IAggregateRoot {

    public MedicalRecordNumber MedicalRecordNumber { get; protected set; }

    public DateOnly DateOfBirth { get; protected set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Gender Gender { get; protected set; }

    public FullName FullName { get; protected set; }

    public List<Allergy> Allergies { get; protected set; } = [];

    public ICollection<OperationRequest> OperationRequests { get; set; } = [];

    private Patient() {

    }

    public Patient(MedicalRecordNumber MedicalRecordNumber, DateOnly DateOfBirth, string Email, string PhoneNumber, Gender Gender, FullName FullName, List<Allergy> Allergies) {
        Id = new PatientId(Guid.NewGuid());
        this.MedicalRecordNumber = MedicalRecordNumber;
        this.DateOfBirth = DateOfBirth;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Gender = Gender;
        this.FullName = FullName;
        this.Allergies = Allergies;
    }

    public static Patient createFromDTO(PatientDTO dto) {
        List<Allergy> allergies = dto.Allergies
            .Split(", ", StringSplitOptions.RemoveEmptyEntries) // Split the string by commas
            .Select(allergyName => new Allergy(allergyName))    // Convert each allergy name into an Allergy object
            .ToList();
        return new Patient(dto.MedicalRecordNumber, dto.DateOfBirth, dto.Email, dto.PhoneNumber, dto.Gender, new FullName(dto.FullName), allergies);
    }
    public PatientDTO returnDTO() {
        return new PatientDTO(MedicalRecordNumber, DateOfBirth, Email, PhoneNumber, Gender, FullName.ToString(), Allergies.ToString());
    }

}
