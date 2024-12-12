using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared;

namespace Backend.Domain.Patients;

public class Patient : Entity<PatientId>, IAggregateRoot {

    public MedicalRecordNumber MedicalRecordNumber { get; protected set; }

    public DateOnly DateOfBirth { get; protected set; }

    public MailAddress Email { get; set; }

    public PhoneNumber PhoneNumber { get; set; }

    public Gender Gender { get; protected set; }

    public FullName FullName { get; set; }

    public List<Allergy> Allergies { get; set; } = [];

    public ICollection<OperationRequest> OperationRequests { get; set; } = [];
    
    public MailAddress UserEmail {get;set;}
    
    public void LinkToAccount(string Email){
        if (UserEmail == null)
            UserEmail = new MailAddress(Email);
        else throw new BusinessRuleValidationException("Can't associate a user to a Patient Profile that already has a user linked to it");
    }

    private Patient() {

    }

    public Patient(MedicalRecordNumber MedicalRecordNumber, DateOnly DateOfBirth, MailAddress Email, PhoneNumber PhoneNumber, Gender Gender, FullName FullName, List<Allergy> Allergies, MailAddress userEmail) {
        Id = new PatientId(Guid.NewGuid());
        this.MedicalRecordNumber = MedicalRecordNumber;
        this.DateOfBirth = DateOfBirth;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Gender = Gender;
        this.FullName = FullName;
        this.Allergies = Allergies;
        this.UserEmail = userEmail;
    }

    public Patient(MedicalRecordNumber MedicalRecordNumber, DateOnly DateOfBirth, MailAddress Email, PhoneNumber PhoneNumber, Gender Gender, FullName FullName, List<Allergy> Allergies) {
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
        return new Patient(new MedicalRecordNumber(dto.MedicalRecordNumber), dto.DateOfBirth, new MailAddress(dto.Email), new PhoneNumber(dto.PhoneNumber), (Gender)Enum.Parse(typeof(Gender), dto.Gender), new FullName(dto.FullName), allergies);
    }
    public PatientDTO returnDTO() {
        return new PatientDTO(MedicalRecordNumber.ToString(), DateOfBirth, Email.ToString(), PhoneNumber.ToString(), Gender.ToString(), FullName.ToString(), string.Join(", ", Allergies.Select(a => a.allergyName)));
    }
    public void SetAllergies(string Allergies){
        List<Allergy> allergies = Allergies
            .Split(", ", StringSplitOptions.RemoveEmptyEntries) // Split the string by commas
            .Select(allergyName => new Allergy(allergyName))    // Convert each allergy name into an Allergy object
            .ToList();
        this.Allergies = allergies;
    }

}
