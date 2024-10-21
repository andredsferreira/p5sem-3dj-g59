using System;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequest : Entity<OperationRequestId>, IAggregateRoot {

    public MedicalRecordNumber patientRecordNumber { get; private set; }

    public StaffId staffId { get; private set; }

    public OperationTypeId operationTypeId { get; private set; }

    public string priority { get; private set; }

    public DateTime dateTime { get; private set; }

    public RequestStatus requestStatus { get; private set; }

    public string teste { get; private set; }

    public OperationRequest(MedicalRecordNumber patientRecordNumber, StaffId staffId, OperationTypeId operationTypeId, string priority, DateTime dateTime, RequestStatus requestStatus) {
        this.Id = new OperationRequestId(Guid.NewGuid());
        this.staffId = staffId;
        this.patientRecordNumber = patientRecordNumber;
        this.operationTypeId = operationTypeId;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

    public OperationRequest(string teste) {
        this.Id = new OperationRequestId(Guid.NewGuid());
        this.teste = teste;
    }

    public static OperationRequest CreateFromDTO(OperationRequestDTO dto) {
        var patientId = dto.patientId;
        var staffId = dto.staffId;
        var operationTypeId = dto.operationTypeId;
        var priority = dto.priority;
        var dateTime = dto.dateTime;
        var requestStatus = dto.requestStatus;
        return new OperationRequest(new MedicalRecordNumber(patientId), new StaffId(staffId), new OperationTypeId(operationTypeId), priority, dateTime, requestStatus);
    }

}

