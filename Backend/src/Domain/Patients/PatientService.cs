using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using Domain.Appointments;

namespace DDDSample1.Domain.Patients;


public class PatientService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPatientRepository _repository;


    public PatientService(IUnitOfWork unitOfWork, IPatientRepository repository) {
        _unitOfWork = unitOfWork;
        _repository = repository;
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

    //public async Task<PatientDTO> EditPatient(string MedicalRecordNumber, PatientDTO dto){
    //    Patient pat = _repository.Get
    //    throw new NotImplementedException();
    //}

    public async Task<PatientDTO> DeletePatient(MedicalRecordNumber id){
            var patient = this._repository.GetPatientByRecordNumber(id);
            Console.WriteLine(patient);
            if (patient == null) return null;
            
            this._repository.Remove(patient);
            await this._unitOfWork.CommitAsync();

            return patient.returnDTO();
        }

    public async Task<IEnumerable<AppointmentDTO>> GetPatientAppointments(string patientEmail) {
        throw new NotImplementedException();
    }

    public async Task<List<Patient>> GetAll() {
        var list = _repository.GetAllAsync();
        return await list;
    }
}

