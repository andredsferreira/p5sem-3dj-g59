using System;
using System.ComponentModel.DataAnnotations;
using Backend.Domain.Appointments;
using Backend.Domain.OperationRequests;
using Backend.Domain.SurgeryRooms;

namespace Domain.Appointments;

public class AppointmentDTO(CreateOperationRequestDTO operationRequestDTO, SurgeryRoomDTO surgeryRoomDTO, DateTime dateTime, AppointmentStatus appointmentStatus) {

    public Guid Id {get;set;} = Guid.NewGuid();
    [Required]
    public CreateOperationRequestDTO OperationRequestDTO {get;set;} = operationRequestDTO;
    [Required]
    public SurgeryRoomDTO SurgeryRoomDTO {get;set;} = surgeryRoomDTO;
    [Required]
    public DateTime DateTime {get;set;} = dateTime;
    [Required]
    public AppointmentStatus AppointmentStatus {get;set;} = appointmentStatus;

}