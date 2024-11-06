using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.SurgeryRooms;

public class SurgeryRoomDTO(RoomType RoomType, RoomStatus RoomStatus, int Capacity, List<string> AssignedEquipment, List<string> MaintenanceSlots) {
    public Guid RoomNumber { get; set; } = Guid.NewGuid();
    [Required]
    public RoomType RoomType { get; set; } = RoomType;
    [Required]
    public RoomStatus RoomStatus { get; set; } = RoomStatus;
    [Required]
    public int Capacity { get; set; } = Capacity;
    [Required]
    public List<string> AssignedEquipment { get; set; } = AssignedEquipment;
    [Required]
    public List<string> MaintenanceSlots { get; set; } = MaintenanceSlots;
}