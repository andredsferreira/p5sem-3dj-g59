using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure.Shared.MessageSender;
using Domain.Appointments;

namespace DDDSample1.Domain.Patients;


public class PatientService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPatientRepository _repository;
    private readonly IMessageSenderService _messageSender;


    public PatientService(IUnitOfWork unitOfWork, IPatientRepository repository, IMessageSenderService messageSender) {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _messageSender = messageSender;
    }

    public async virtual Task<PatientDTO> CreatePatient(PatientDTO dto) {
        dto.MedicalRecordNumber = await GenerateMedicalRecord();
        var patient = Patient.createFromDTO(dto);
        await this._repository.AddAsync(patient);
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

    public async Task<PatientDTO> EditPatient(MedicalRecordNumber id, FilterPatientDTO dto){
        var patient = this._repository.GetPatientByRecordNumber(id);
        if (patient == null) return null;

        bool warn = false;
        string email = patient.Email;
        StringBuilder builder = new(string.Format("Hello {0},\nThis message was sent to warn you that:\n", patient.FullName.Full));

        if (!string.IsNullOrEmpty(dto.FullName))
            patient.FullName = new FullName(dto.FullName);

        if (!string.IsNullOrEmpty(dto.PhoneNumber)){
            warn = true;
            builder.Append(string.Format("The Phone Number associated with you was changed from {0} to {1}.\n",patient.PhoneNumber,dto.PhoneNumber));
            patient.PhoneNumber = dto.PhoneNumber;
        }
        if (!string.IsNullOrEmpty(dto.Email)){
            warn = true;
            builder.Append(string.Format("The Email associated with you was changed from {0} to {1}.\n",patient.Email,dto.Email));
            patient.Email = dto.Email;
        }

        this._repository.Update(patient);
        await this._unitOfWork.CommitAsync();

        if (warn){
            builder.Append("\nThis message was sent automatically. Don't answer it.\n");
            _messageSender.SendMessage(email, "Some of your data was altered", builder.ToString());
        }

        return patient.returnDTO();
    }

    public async Task<PatientDTO> DeletePatient(MedicalRecordNumber id){
        var patient = this._repository.GetPatientByRecordNumber(id);
        if (patient == null) return null;
        
        this._repository.Remove(patient);
        await this._unitOfWork.CommitAsync();

        return patient.returnDTO();
    }

    public async Task<IEnumerable<Patient>> SearchPatients(FilterPatientDTO filterPatientDTO)
    {
        var patients = await GetAll();

        if (filterPatientDTO.MedicalRecordNumber != null)
            patients = patients.Where(p => p.MedicalRecordNumber.Record.Equals(filterPatientDTO.MedicalRecordNumber));
        if (!string.IsNullOrEmpty(filterPatientDTO.Email))
            patients = patients.Where(p => p.Email.Contains(filterPatientDTO.Email, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(filterPatientDTO.PhoneNumber))
            patients = patients.Where(p => p.PhoneNumber.Contains(filterPatientDTO.PhoneNumber));
        if (!string.IsNullOrEmpty(filterPatientDTO.FullName))
            patients = patients.Where(p => p.FullName.Full.Contains(filterPatientDTO.FullName, StringComparison.OrdinalIgnoreCase));
        if (filterPatientDTO.Gender.HasValue)
            patients = patients.Where(p => p.Gender.Equals(filterPatientDTO.Gender));

        return patients;
    }


    public async Task<IEnumerable<AppointmentDTO>> GetPatientAppointments(string patientEmail) {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Patient>> GetAll() {
        var list = _repository.GetAllAsync();
        return await list;
    }
}

