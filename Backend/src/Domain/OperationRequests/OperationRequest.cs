using System;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequest : Entity<OperationRequestId>, IAggregateRoot {

    public PatientId patientId { get; private set; }

    public Patient patient { get; set; }

    public StaffId staffId { get; }

    public Staff staff { get; set; }

    public OperationTypeId operationTypeId { get; set; }

    public OperationType operationType { get; set; }

    public string priority { get; set; }

    public DateTime dateTime { get; set; }

    public RequestStatus requestStatus { get; set; }

    public OperationRequest(PatientId patientId, OperationTypeId operationTypeId, string priority, DateTime dateTime, RequestStatus requestStatus) {
        Id = new OperationRequestId(Guid.NewGuid());
        this.patientId = patientId;
        this.operationTypeId = operationTypeId;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

    public static OperationRequest CreateFromDTO(OperationRequestDTO dto) {
        var patientId = dto.patientId;
        var operationTypeId = dto.operationTypeId;
        var priority = dto.priority;
        var dateTime = dto.dateTime;
        var requestStatus = dto.requestStatus;
        var operationRequest = new OperationRequest(new PatientId(patientId), new OperationTypeId(operationTypeId), priority, dateTime, requestStatus);
        return operationRequest;
    }

    public static OperationRequestDTO returnDTO(OperationRequest or) {
        throw new NotImplementedException();
    }

}
