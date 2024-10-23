using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using Domain.Appointments;

namespace DDDSample1.Domain.Patients;


public class MedicalRecordNumberGenerator {

    private readonly IPatientRepository _repository;


    public MedicalRecordNumberGenerator(IPatientRepository repository) {
        _repository = repository;
    }

    public MedicalRecordNumber CreateMedicalRecord(){
        return null; //Not done yet
    }

}