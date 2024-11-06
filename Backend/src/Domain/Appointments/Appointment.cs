using System;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared;
using Backend.Domain.SurgeryRooms;
using Domain.Appointments;

namespace Backend.Domain.Appointments;

public class Appointment : Entity<AppointmentId>, IAggregateRoot {

    public OperationRequest OperationRequest { get; set; }
    public SurgeryRoom SurgeryRoom { get; set; }
    public DateTime DateTime { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }

    public Appointment(OperationRequest operationRequest, SurgeryRoom surgeryRoom, DateTime dateTime, AppointmentStatus appointmentStatus) {
        Id = new AppointmentId(Guid.NewGuid());
        this.OperationRequest = operationRequest;
        this.SurgeryRoom = surgeryRoom;
        this.DateTime = dateTime;
        this.AppointmentStatus = appointmentStatus;
    }
    public static Appointment CreateFromDTO(AppointmentDTO dto) {
        return new Appointment(OperationRequest.CreateFromDTO(dto.OperationRequestDTO),
                                SurgeryRoom.CreateFromDTO(dto.SurgeryRoomDTO),
                                dto.DateTime,
                                dto.AppointmentStatus);
    }

    public static AppointmentDTO returnDTO(Appointment appointment) {
        throw new NotImplementedException();
    }
}
