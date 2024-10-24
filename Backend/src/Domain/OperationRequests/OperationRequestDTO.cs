using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestDTO {

    public Guid id { get; set; }

    [Required]
    [FromForm(Name = "patientId")]
    public Guid patientId { get; set; }

    [Required]
    [FromForm(Name = "operationTypeId")]
    public Guid operationTypeId { get; set; }

    [Required]
    [FromForm(Name = "priority")]
    public string priority { get; private set; }

    [Required]
    [FromForm(Name = "dateTime")]
    public DateTime dateTime { get; private set; }

    [Required]
    [FromForm(Name = "requestStatus")]
    public RequestStatus requestStatus { get; private set; }

    public OperationRequestDTO(Guid id, Guid patientId, Guid operationTypeId, string priority, DateTime dateTime, RequestStatus requestStatus) {
        this.id = id;
        this.patientId = patientId;
        this.operationTypeId = operationTypeId;
        this.priority = priority;
        this.dateTime = dateTime;
        this.requestStatus = requestStatus;
    }

}