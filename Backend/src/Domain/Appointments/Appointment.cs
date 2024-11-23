using System;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared;
using Backend.Domain.SurgeryRooms;
using Domain.Appointments;

namespace Backend.Domain.Appointments;

public class Appointment : Entity<AppointmentId>, IAggregateRoot {

    public OperationRequestId OperationRequestId {get;set;}
    public OperationRequest OperationRequest { get; set; }
    public SurgeryRoomId SurgeryRoomId {get;set;}
    public SurgeryRoom SurgeryRoom { get; set; }
    public DateTime DateTime { get; set; }
    public AppointmentStatus AppointmentStatus { get; set; }

    // public static Appointment CreateFromDTO(AppointmentDTO dto) {
    //     return new Appointment(OperationRequest.CreateFromDTO(dto.OperationRequestDTO),
    //                             SurgeryRoom.CreateFromDTO(dto.SurgeryRoomDTO),
    //                             dto.DateTime,
    //                             dto.AppointmentStatus);
    // }

    public static AppointmentDTO returnDTO(Appointment appointment) {
        throw new NotImplementedException();
    }
    public DateTime EndDateTime(){
        Console.WriteLine("DateTime = " + DateTime);
        return DateTime.AddMinutes(OperationRequest.operationType.anaesthesiaTime.duration
                                    + OperationRequest.operationType.surgeryTime.duration
                                    + OperationRequest.operationType.cleaningTime.duration);
    }
}
