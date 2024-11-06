using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestDTO {

    public Guid operationRequestId { get; set; }

    public Guid staffId { get; set; }

    [Required]
    public Guid patientId { get; set; }

    [Required]
    public Guid operationTypeId { get; set; }

    [Required]
    public string priority { get; set; }

    [Required]
    public DateTime dateTime { get; set; }

    [Required]
    public string requestStatus { get; set; }

    public OperationRequestDTO(Guid operationRequestId = default, Guid staffId = default,
        Guid patientId = default,
        Guid operationTypeId = default,
        string priority = null,
        DateTime dateTime = default,
        string requestStatus = null) {
            this.operationRequestId = operationRequestId != default ? operationRequestId : Guid.NewGuid();
            this.staffId = staffId;
            this.patientId = patientId != default ? patientId : throw new ArgumentNullException(nameof(patientId));
            this.operationTypeId = operationTypeId != default ? operationTypeId : throw new ArgumentNullException(nameof(operationTypeId));
            this.priority = priority ?? throw new ArgumentNullException(nameof(priority));
            this.dateTime = dateTime != default ? dateTime : DateTime.UtcNow;
            this.requestStatus = requestStatus ?? throw new ArgumentNullException(nameof(requestStatus));
    }

}