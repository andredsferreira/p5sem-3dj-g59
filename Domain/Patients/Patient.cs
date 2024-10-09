using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Patients {

    public class Patient : Entity<MedicalRecordNumber>, IAggregateRoot {
        
    }
}