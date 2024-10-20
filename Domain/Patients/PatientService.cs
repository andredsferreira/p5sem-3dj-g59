using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using Domain.Appointments;
using Microsoft.AspNetCore.Identity;

namespace DDDSample1.Domain.Patients;


public class PatientService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IPatientRepository _repository;


    public PatientService(IUnitOfWork unitOfWork, IPatientRepository repository) {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<PatientDTO> CreatePatient(PatientDTO dto) {
        var patient = Patient.createFromDTO(dto);
        await this._repository.AddAsync(patient);
        await this._unitOfWork.CommitAsync();

        return dto;
    }

    public async Task<IEnumerable<AppointmentDTO>> GetPatientAppointments(string patientEmail) {
        throw new NotImplementedException();
    }
}

