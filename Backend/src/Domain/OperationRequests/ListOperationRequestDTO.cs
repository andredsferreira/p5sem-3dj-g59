using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.OperationRequests;

public class ListOperationRequestDTO {

    [Required]
    public Guid operationRequestId { get; set; }

    [Required]
    public string doctorName {get; set;}

    [Required]
    public Guid patientId { get; set; }

    [Required]
    public string patientFullName { get; set; }

    [Required]
    public string operationTypeName { get; set; }

    [Required]
    public string priority { get; set; }

    [Required]
    public DateTime dateTime { get; set; }

    [Required]
    public string requestStatus { get; set; }

}