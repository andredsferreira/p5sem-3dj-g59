using System;
using System.Collections.Generic;
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

    public async Task<PatientDTO> CreatePatient(PatientDTO dto) {
        dto.MedicalRecordNumber = GenerateMedicalRecord();
        var patient = Patient.createFromDTO(dto);
        await this._repository.AddAsync(patient);
        await this._unitOfWork.CommitAsync();

        return dto;
    }
    private MedicalRecordNumber GenerateMedicalRecord(){
        return null;
    }
    
    public async Task<PatientDTO> DeletePatient(PatientId id){
            var patient = await this._repository.GetByIdAsync(id);
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

