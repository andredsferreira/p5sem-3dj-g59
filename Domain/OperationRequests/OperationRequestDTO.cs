using System;
using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestDTO {

    public Guid id { get; set; }

    [Required]
    public Guid patientId { get; set; }

    [Required]
    public Guid staffId { get; set; }

    [Required]
    public Guid operationTypeId { get; set; }

    [Required]
    public string priority { get; private set; }

    [Required]
    public DateTime dateTime { get; private set; }

    [Required]
    public RequestStatus requestStatus { get; private set; }

    public OperationRequestDTO(Guid id, Guid patientId, Guid staffId, Guid operationTypeId, string priority, DateTime dateTime, RequestStatus requestStatus) {
        this.id = id;
        this.patientId = patientId;
        this.staffId = staffId;
        this.operationTypeId = operationTypeId;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

}