using System.Net.Mail;
using System.Threading.Tasks;
using Backend.Domain.Shared;

namespace Backend.Domain.Patients;

public interface IPatientRepository : IRepository<Patient, PatientId> {

    public Patient GetPatientByRecordNumber(MedicalRecordNumber recordNumber);

    public Patient GetByEmail(MailAddress email);

    public Task<Patient> GetByUserEmail(MailAddress email);
    
}
