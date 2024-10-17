using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients {

    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot {
        public DateOnly DateOfBirth {get; private set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}
        public FullName FullName {get; set;}
        public List<string> Allergies {get; set;}

        private Patient() {}

        public Patient(DateOnly DateOfBirth, string Email, string PhoneNumber, FullName FullName, List<string> Allergies){
            Id = new MedicalRecordNumber(Guid.NewGuid());
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.FullName = FullName;
            this.Allergies = Allergies;
        }

        public static Patient createFromDTO(PatientDTO dto) {
            return new Patient(dto.DateOfBirth, dto.Email, dto.PhoneNumber, new FullName(dto.FullName), [.. dto.Allergies.Split(", ")]);
        }        
        
    }
}