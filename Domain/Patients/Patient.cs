using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients {

    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot {

        public static Patient createFromDTO(PatientDTO dto) {
            throw new NotImplementedException();
        }        
        
    }
}