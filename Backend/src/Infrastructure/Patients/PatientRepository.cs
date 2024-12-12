using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Backend.Domain.Patients;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Patients;

public class PatientRepository : BaseRepository<Patient, PatientId>, IPatientRepository {

#nullable disable

    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context) : base(context.Patients) {
        _context = context;
    }

    public Patient GetPatientByRecordNumber(MedicalRecordNumber record) {
        return _context.Patients.Where(p => p.MedicalRecordNumber.Equals(record)).SingleOrDefault();
    }

    public Patient GetByEmail(MailAddress email) {
        return _context.Patients.Where(p => p.Email.Equals(email)).SingleOrDefault();
    }

    public async Task<Patient> GetByUserEmail(MailAddress email) {
        return await _context.Patients
        .Where(p => p.UserEmail.Equals(email))
        .SingleOrDefaultAsync();
    }
}

