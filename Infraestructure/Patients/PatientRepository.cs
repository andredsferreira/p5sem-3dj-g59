using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Patients {

    public class PatientRepository : BaseRepository<Patient, MedicalRecordNumber>, IPatientRepository {

        public PatientRepository(DbSet<Patient> objs) : base(objs) {
            
        }
    }

}