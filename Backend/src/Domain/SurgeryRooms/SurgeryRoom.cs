using System;
using System.Collections.Generic;
using Backend.Domain.Shared;

namespace Backend.Domain.SurgeryRooms;

public class SurgeryRoom : Entity<SurgeryRoomId>, IAggregateRoot {

    public RoomNumber Number {get; set;}
    public RoomType RoomType { get; set; }
    public RoomStatus RoomStatus { get; set; }
    public int Capacity { get; set; }
    public List<string> AssignedEquipment { get; set; }
    public List<string> MaintenanceSlots { get; set; }

    public SurgeryRoom(RoomNumber Number, RoomType RoomType, RoomStatus RoomStatus, int Capacity, List<string> AssignedEquipment, List<string> MaintenanceSlots) {
        Id = new SurgeryRoomId(Guid.NewGuid());
        this.Number = Number;
        this.RoomType = RoomType;
        this.RoomStatus = RoomStatus;
        this.Capacity = Capacity;
        this.AssignedEquipment = AssignedEquipment;
        this.MaintenanceSlots = MaintenanceSlots;
    }

    public static SurgeryRoom CreateFromDTO(SurgeryRoomDTO dto) {
        return new SurgeryRoom(dto.Number, dto.RoomType, dto.RoomStatus, dto.Capacity, dto.AssignedEquipment, dto.MaintenanceSlots);
    }
    public SurgeryRoomDTO ReturnDTO() {
        return new SurgeryRoomDTO(Number, RoomType, RoomStatus, Capacity, AssignedEquipment, MaintenanceSlots);
    }
}

