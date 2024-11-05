using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestDTO {

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

    public OperationRequestDTO() {

    }

    // Constructor for creating operation request.
    public OperationRequestDTO(Guid patientId, Guid operationTypeId, string priority, DateTime dateTime, string requestStatus) {
        this.patientId = patientId;
        this.operationTypeId = operationTypeId;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

    // Constructor for listing operation requests.
    public OperationRequestDTO(Guid id, Guid patientId, Guid staffId, Guid operationTypeId, string priority, DateTime dateTime, string requestStatus) {
        this.patientId = patientId;
        this.operationTypeId = operationTypeId;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

}