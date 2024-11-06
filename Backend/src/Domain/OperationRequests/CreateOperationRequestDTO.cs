using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.OperationRequests;

public class CreateOperationRequestDTO {

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

}