using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Appointments;

public class UpdateAppointmentDTO {
    
    [Required]
    public Guid updatedId { get; set; }

    public DateTime? dateTime { get; set; }

    public int roomNumber { get; set; }

    public string appointmentStatus { get; set; }
    
}
