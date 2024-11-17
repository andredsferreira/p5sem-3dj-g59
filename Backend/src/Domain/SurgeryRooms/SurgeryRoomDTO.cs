using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.SurgeryRooms;

public class SurgeryRoomDTO(int Number, RoomType RoomType, RoomStatus RoomStatus, int Capacity, List<string> AssignedEquipment, List<string> Slots) {
    
    [Required]
    public int Number {get; set;} = Number;
    [Required]
    public RoomType RoomType { get; set; } = RoomType;
    [Required]
    public RoomStatus RoomStatus { get; set; } = RoomStatus;
    [Required]
    public int Capacity { get; set; } = Capacity;
    [Required]
    public List<string> AssignedEquipment { get; set; } = AssignedEquipment;
    [Required]
    public List<string> Slots { get; set; } = Slots;
}