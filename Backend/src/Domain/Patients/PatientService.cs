using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DDDSample1.Domain.DomainLogs;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure.Shared.MessageSender;
using Domain.Appointments;

namespace DDDSample1.Domain.Patients;


public class PatientService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPatientRepository _repository;
    private readonly IDomainLogRepository _logRepository;
    private readonly IMessageSenderService _messageSender;


    public PatientService(IUnitOfWork unitOfWork, IPatientRepository repository, IDomainLogRepository logRepository, IMessageSenderService messageSender) {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _logRepository = logRepository;
        _messageSender = messageSender;
    }

    public async virtual Task<PatientDTO> CreatePatient(PatientDTO dto) {
        dto.MedicalRecordNumber = (await GenerateMedicalRecord()).ToString();
        var patient = Patient.createFromDTO(dto);
        await this._repository.AddAsync(patient);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Patient, LogActionType.Creation, 
            string.Format("Created a new Patient (Medical Record Number = {0}, Name = {1}, Email = {2}, PhoneNumber = {3})",
                        patient.MedicalRecordNumber.Record, patient.FullName.Full, patient.Email, patient.PhoneNumber)));
        await this._unitOfWork.CommitAsync();
        return dto;
    }
    private async Task<MedicalRecordNumber> GenerateMedicalRecord(){
        StringBuilder stringBuilder = new(DateTime.Today.Year.ToString());
        stringBuilder.Append(DateTime.Today.Month);
        var sequentialNumber = (await _repository.GetAllAsync())
            .Where(p => p.MedicalRecordNumber.Record.StartsWith(stringBuilder.ToString()))
            .Select(p => int.Parse(p.MedicalRecordNumber.Record.Substring(6, 6)))
            .DefaultIfEmpty(0)
            .Max() + 1;
        stringBuilder.Append(string.Format("{0:D6}", sequentialNumber));
        return new MedicalRecordNumber(stringBuilder.ToString());
    }

    public PatientDTO GetPatientById(MedicalRecordNumber id) {
        var patient = this._repository.GetPatientByRecordNumber(id);
        if (patient == null) return null;
        return patient.returnDTO();
    }

    public async virtual Task<PatientDTO> EditPatient(MedicalRecordNumber id, FilterPatientDTO dto){
        var patient = this._repository.GetPatientByRecordNumber(id);
        if (patient == null) return null;

        bool warn = false;
        string email = patient.Email.ToString();
        StringBuilder messageBuilder = new(string.Format("Hello {0},<br>This message was sent to warn you that:<br>", patient.FullName.Full)),
            logBuilder = new(string.Format("Edit in Patient {0}: ", id.Record));

        if (!string.IsNullOrEmpty(dto.FullName)){
            logBuilder.Append(string.Format("Full name changed from {0} to {1}, ", patient.FullName.Full, dto.FullName));
            patient.FullName = new FullName(dto.FullName);
        }

        if (!string.IsNullOrEmpty(dto.PhoneNumber)){
            warn = true;
            logBuilder.Append(string.Format("Phone Number changed from {0} to {1}, ", patient.PhoneNumber, dto.PhoneNumber));
            messageBuilder.Append(string.Format("-The Phone Number associated with your account was changed from {0} to {1}.<br>",patient.PhoneNumber,dto.PhoneNumber));
            patient.PhoneNumber = new PhoneNumber(dto.PhoneNumber);
        }
        if (!string.IsNullOrEmpty(dto.Email)){
            warn = true;
            logBuilder.Append(string.Format("Email changed from {0} to {1}, ", patient.Email, dto.Email));
            messageBuilder.Append(string.Format("-The Email associated with your account was changed from {0} to {1}.<br>",patient.Email,dto.Email));
            patient.Email = new MailAddress(dto.Email);
        }
        if (!string.IsNullOrEmpty(dto.Allergies)){
            logBuilder.Append(string.Format("Allergies changed from '{0}' to '{1}'", string.Join(", ", patient.Allergies.Select(a => a.allergyName)), dto.Allergies));
            patient.SetAllergies(dto.Allergies);
        }

        this._repository.Update(patient);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Patient, LogActionType.Edit, logBuilder.ToString()));
        await this._unitOfWork.CommitAsync();

        if (warn){
            messageBuilder.Append("<br>This message was sent automatically. Don't answer it.<br>");
            _messageSender.SendMessage(email, "Some of your data was altered", messageBuilder.ToString());
        }

        return patient.returnDTO();
    }

    public async Task<PatientDTO> EditPatientByAdmin(MedicalRecordNumber id, FilterPatientDTO dto){
        var patient = this._repository.GetPatientByRecordNumber(id);
        if (patient == null) return null;

        bool warn = false;
        string email = patient.Email.ToString();
        StringBuilder messageBuilder = new(string.Format("Hello {0},<br>This message was sent to warn you that:<br>", patient.FullName.Full)),
            logBuilder = new(string.Format("Edit in Patient {0}: ", id.Record));

        if (!string.IsNullOrEmpty(dto.FullName)){
            logBuilder.Append(string.Format("Full name changed from {0} to {1}, ", patient.FullName.Full, dto.FullName));
            patient.FullName = new FullName(dto.FullName);
        }

        if (!string.IsNullOrEmpty(dto.PhoneNumber)){
            warn = true;
            logBuilder.Append(string.Format("Phone Number changed from {0} to {1}, ", patient.PhoneNumber, dto.PhoneNumber));
            messageBuilder.Append(string.Format("-The Phone Number associated with your account was changed from {0} to {1}.<br>",patient.PhoneNumber,dto.PhoneNumber));
            patient.PhoneNumber = new PhoneNumber(dto.PhoneNumber);
        }
        if (!string.IsNullOrEmpty(dto.Email)){
            warn = true;
            logBuilder.Append(string.Format("Email changed from {0} to {1}, ", patient.Email, dto.Email));
            messageBuilder.Append(string.Format("-The Email associated with your account was changed from {0} to {1}.<br>",patient.Email,dto.Email));
            patient.Email = new MailAddress(dto.Email);
        }

        // send an email with a link to confirm the changes made to a profile
        
        /*
        messageBuilder.Append("<br>To confirm this changes, please click <a href='http://localhost:5000/api/patients/confirmEdit'>here</a> to cancel them.<br>");
        messageBuilder.Append("<br>This message was sent automatically. Don't answer it.<br>");
        _messageSender.SendMessage(email, "Some of your data was altered", messageBuilder.ToString());
        */
        
        // wait for the confirmation
        // if the user confirms the changes, the changes are saved
        // if the user cancels the changes, the changes are not saved

        

        return patient.returnDTO();
    }

    public virtual async Task<PatientDTO> DeletePatient(MedicalRecordNumber id){
        var patient = this._repository.GetPatientByRecordNumber(id);
        if (patient == null) return null;
        
        this._repository.Remove(patient);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Patient, LogActionType.Deletion, 
            string.Format("Deleted Patient with Medical Record Number = {0}", patient.MedicalRecordNumber.Record)));
        await this._unitOfWork.CommitAsync();

        return patient.returnDTO();
    }

    public async Task<IEnumerable<PatientDTO>> SearchPatients(FilterPatientDTO filterPatientDTO)
    {
        var patients = await GetAll();

        if (!string.IsNullOrEmpty(filterPatientDTO.MedicalRecordNumber))
            patients = patients.Where(p => p.MedicalRecordNumber.Equals(filterPatientDTO.MedicalRecordNumber));
        if (!string.IsNullOrEmpty(filterPatientDTO.Email))
            patients = patients.Where(p => p.Email.Contains(filterPatientDTO.Email, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(filterPatientDTO.PhoneNumber))
            patients = patients.Where(p => p.PhoneNumber.Contains(filterPatientDTO.PhoneNumber));
        if (!string.IsNullOrEmpty(filterPatientDTO.FullName))
            patients = patients.Where(p => p.FullName.Contains(filterPatientDTO.FullName, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(filterPatientDTO.Gender))
            patients = patients.Where(p => p.Gender.Equals(filterPatientDTO.Gender));

        return patients;
    }


    public async Task<IEnumerable<AppointmentDTO>> GetPatientAppointments(string patientEmail) {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PatientDTO>> GetAll() {
        var list = _repository.GetAllAsync();
        return (await list).Select(patient => patient.returnDTO());
    }
}

