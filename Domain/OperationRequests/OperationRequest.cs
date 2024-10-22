using System;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequest : Entity<OperationRequestId>, IAggregateRoot {

    public Patient patient { get; private set; }

    public Guid staffId { get; set; }

    public Staff staff { get; private set; }

    public Guid operationTypeId { get; set; }

    public OperationType operationType { get; private set; }

    public string priority { get; private set; }

    public DateTime dateTime { get; private set; }

    public RequestStatus requestStatus { get; private set; }

    public string teste { get; private set; }

    public OperationRequest(Patient patient, Staff staff, OperationType operationType, string priority, DateTime dateTime, RequestStatus requestStatus) {
        this.Id = new OperationRequestId(Guid.NewGuid());
        this.staff = staff;
        this.patient = patient;
        this.operationType = operationType;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

    public OperationRequest(string teste) {
        this.Id = new OperationRequestId(Guid.NewGuid());
        this.teste = teste;
    }

    public static OperationRequest CreateFromDTO(OperationRequestDTO dto) {
        var patient = dto.patient;
        var staff = dto.staff;
        var operationType = dto.operationType;
        var priority = dto.priority;
        var dateTime = dto.dateTime;
        var requestStatus = dto.requestStatus;
        return new OperationRequest(Patient.createFromDTO(patient), Staff.createFromDTO(staff), OperationType.createFromDTO(operationType), priority, dateTime, requestStatus);
    }

    public static OperationRequestDTO returnDTO(OperationRequest or) {
        throw new NotImplementedException();
    }

}

