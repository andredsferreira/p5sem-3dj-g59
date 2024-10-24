using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Patients;

public class PatientRepository : BaseRepository<Patient, PatientId>, IPatientRepository {

    private readonly DDDSample1DbContext _context;

    public PatientRepository(DDDSample1DbContext context) : base(context.Patients) {
        _context = context;
    }
    
    public Patient GetPatientByRecordNumber(MedicalRecordNumber record){
        return _context.Patients.Where(p => p.MedicalRecordNumber.Equals(record)).SingleOrDefault();
    }
}

