using System;
using System.Collections.Generic;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared;
using Backend.Domain.Specializations;
using Backend.Infrastructure.Specializations;

namespace Backend.Domain.OperationTypes;

public class OperationType : Entity<OperationTypeId>, IAggregateRoot {

    public OperationName name { get; set; }

    public AnaesthesiaTime anaesthesiaTime { get; set; }

    public SurgeryTime surgeryTime { get; set; }

    public CleaningTime cleaningTime { get; set; }

    public ICollection<OperationRequest> OperationRequests { get; set; }
    
    public Status Status { get; set; }

    public Specialization Specialization { get; set; }
    public int MinDoctor { get; set; }
    public int MinAnesthetist { get; set; }
    public int MinInstrumentingNurse { get; set; }
    public int MinCirculatingNurse { get; set; }
    public int MinNurseAnaesthetist { get; set; }
    public int MinXRayTechnician { get; set; }
    public int MinMedicalActionAssistant { get; set; }

    //TO REMOVE
    public OperationType(OperationName name, AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime){
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
    }

    public OperationType(OperationName name) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = new AnaesthesiaTime(0);
        this.surgeryTime = new SurgeryTime(0);
        this.cleaningTime = new CleaningTime(0);
        this.Status = Status.ACTIVE;
        this.Specialization = new Specialization();
        this.MinDoctor = 1;
        this.MinAnesthetist = 1;
        this.MinInstrumentingNurse = 1;
        this.MinCirculatingNurse = 1;
        this.MinNurseAnaesthetist = 1;
        this.MinXRayTechnician = 1;
        this.MinMedicalActionAssistant = 1;
    }

    public OperationType(OperationName name, AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime, Specialization specialization) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Specialization = specialization;
        this.Status = Status.ACTIVE;
    }

    public OperationType(OperationName name, AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime, Status Status) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
    }

    public OperationType(OperationName name, AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime, Status Status, Specialization specialization, int minDoctor, int minAnesthetist, int minInstrumentingNurse, int minCirculatingNurse, int minNurseAnaesthetist, int minXRayTechnician, int minMedicalActionAssistant) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
        this.Specialization = specialization;
        this.MinDoctor = minDoctor;
        this.MinAnesthetist = minAnesthetist;
        this.MinInstrumentingNurse = minInstrumentingNurse;
        this.MinCirculatingNurse = minCirculatingNurse;
        this.MinNurseAnaesthetist = minNurseAnaesthetist;
        this.MinXRayTechnician = minXRayTechnician;
        this.MinMedicalActionAssistant = minMedicalActionAssistant;
    }
    public static OperationType createFromDTO(OperationTypeDTO dto, ISpecializationRepository specializationRepository) {
        OperationName name = new OperationName(dto.name);
        AnaesthesiaTime anaesthesiaTime = new AnaesthesiaTime(dto.anaesthesiaTime);
        SurgeryTime surgeryTime = new SurgeryTime(dto.surgeryTime);
        CleaningTime cleaningTime = new CleaningTime(dto.cleaningTime);
        Status status = (Status)Enum.Parse(typeof(Status), dto.Status);
        //get specialization from id
        Specialization specialization = specializationRepository.GetByIdAsync(new SpecializationID(dto.SpecializationId)).Result;
        int minDoctor = dto.minDoctor;
        int minAnesthetist = dto.minAnaesthetist;
        int minInstrumentingNurse = dto.minInstrumentingNurse;
        int minCirculatingNurse = dto.minCirculatingNurse;
        int minNurseAnaesthetist = dto.minNurseAnaesthetist;
        int minXRayTechnician = dto.minXRayTechnician;
        int minMedicalActionAssistant = dto.minMedicalActionAssistant;

        return new OperationType(name, anaesthesiaTime, surgeryTime, cleaningTime, status, specialization, minDoctor, minAnesthetist,
            minInstrumentingNurse, minCirculatingNurse, minNurseAnaesthetist, minXRayTechnician, minMedicalActionAssistant);

    }



    public OperationTypeDTO returnDTO() {

        return new OperationTypeDTO(Id.AsGuid(), name.ToString(), anaesthesiaTime.duration, surgeryTime.duration, cleaningTime.duration,
            Status.ToString(), Specialization.Id.AsString(), MinDoctor, MinAnesthetist, MinInstrumentingNurse, MinCirculatingNurse, MinNurseAnaesthetist,
            MinXRayTechnician, MinMedicalActionAssistant);
    }


}

