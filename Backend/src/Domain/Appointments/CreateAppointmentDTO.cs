using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Backend.Domain.OperationRequests;

public class CreateAppointmentDTO {

    [Required]
    public Guid operationRequestId { get; set; }

    [Required]
    public int roomNumber { get; set; }

    [Required]
    public DateTime dateTime { get; set; }

    [Required]
    public string appointmentStatus { get; set; }

}