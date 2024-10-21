using System;
using System.ComponentModel.DataAnnotations;
using DDDSample1.Domain.Appointments;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.SurgeryRooms;

namespace Domain.Appointments;

public class AppointmentDTO(OperationRequestDTO operationRequestDTO, SurgeryRoomDTO surgeryRoomDTO, DateTime dateTime, AppointmentStatus appointmentStatus) {

    public Guid Id {get;set;} = Guid.NewGuid();
    [Required]
    public OperationRequestDTO OperationRequestDTO {get;set;} = operationRequestDTO;
    [Required]
    public SurgeryRoomDTO SurgeryRoomDTO {get;set;} = surgeryRoomDTO;
    [Required]
    public DateTime DateTime {get;set;} = dateTime;
    [Required]
    public AppointmentStatus AppointmentStatus {get;set;} = appointmentStatus;

}