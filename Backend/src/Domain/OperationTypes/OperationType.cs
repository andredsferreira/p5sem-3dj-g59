using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared;


namespace DDDSample1.Domain.OperationTypes;

public class OperationType : Entity<OperationTypeId>, IAggregateRoot {

    public OperationName name { get; private set; }

    public AnaesthesiaTime anaesthesiaTime { get; private set; }

    public SurgeryTime surgeryTime { get; private set; }

    public CleaningTime cleaningTime { get; private set; }

    public ICollection<OperationRequest> OperationRequests { get; set; }
    public Status Status{get; set;}

    public OperationType(OperationName name) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = new AnaesthesiaTime(0);
        this.surgeryTime = new SurgeryTime(0);
        this.cleaningTime = new CleaningTime(0);
    }

    public OperationType(OperationName name, AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
    }

    public OperationType(OperationName name, AnaesthesiaTime anaesthesiaTime, SurgeryTime surgeryTime, CleaningTime cleaningTime, Status Status) {
        Id = new OperationTypeId(Guid.NewGuid());
        this.name = name;
        this.anaesthesiaTime = anaesthesiaTime;
        this.surgeryTime = surgeryTime;
        this.cleaningTime = cleaningTime;
        this.Status = Status;
    }


    public static OperationType createFromDTO(OperationTypeDTO dto) {
        OperationName name = new OperationName(dto.name);
        AnaesthesiaTime anaesthesiaTime = new AnaesthesiaTime(dto.anaesthesiaTime);
        SurgeryTime surgeryTime = new SurgeryTime(dto.surgeryTime);
        CleaningTime cleaningTime = new CleaningTime(dto.cleaningTime);
        Status status = (Status)Enum.Parse(typeof(Status),dto.Status);
        return new OperationType(name, anaesthesiaTime, surgeryTime, cleaningTime, status);
    }

    public OperationTypeDTO returnDTO() {
        
        return new OperationTypeDTO(name.ToString(), anaesthesiaTime.duration, surgeryTime.duration, cleaningTime.duration, Status.ToString());
    }


}

