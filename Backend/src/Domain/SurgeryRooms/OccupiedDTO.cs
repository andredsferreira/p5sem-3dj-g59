using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.SurgeryRooms;

public class OccupiedDTO {
    public int RoomNumber {get;set;}
    public DateTime Begin {get;set;}
    public DateTime End {get;set;}
    public RoomStatus Status {get;set;}
    public string PatientName {get;set;}
    public string PatientMRN {get;set;}
}