using System.Net.Mail;
using Backend.Domain.Shared;

namespace Backend.Domain.Patients;

public interface IPatientRepository : IRepository<Patient, PatientId> {

    public Patient GetPatientByRecordNumber(MedicalRecordNumber recordNumber);

    public Patient GetByEmail(MailAddress email);

    public Patient GetByUserEmail(MailAddress email);
    
}
