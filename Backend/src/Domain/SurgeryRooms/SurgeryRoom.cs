using System;
using System.Collections.Generic;
using Backend.Domain.Shared;
using Backend.Domain.Slots;

namespace Backend.Domain.SurgeryRooms;

public class SurgeryRoom : Entity<SurgeryRoomId>, IAggregateRoot {

    public RoomNumber Number {get; set;}
    public RoomType RoomType { get; set; }
    public RoomStatus RoomStatus { get; set; }
    public int Capacity { get; set; }
    public List<string> AssignedEquipment { get; set; }
    public List<DaySlots> MaintenanceSlots {get; set;}
    private SurgeryRoom(){}

    public SurgeryRoom(RoomNumber Number, RoomType RoomType, RoomStatus RoomStatus, int Capacity, List<string> AssignedEquipment, List<DaySlots> MaintenanceSlots) {
        Id = new SurgeryRoomId(Guid.NewGuid());
        this.Number = Number;
        this.RoomType = RoomType;
        this.RoomStatus = RoomStatus;
        this.Capacity = Capacity;
        this.AssignedEquipment = AssignedEquipment;
        this.MaintenanceSlots = MaintenanceSlots;
    }

    public static SurgeryRoom CreateFromDTO(SurgeryRoomDTO dto) {
        List<DaySlots> slots = [];
        foreach(string s in dto.Slots)
            slots.Add(new DaySlots(s));
        return new SurgeryRoom(new RoomNumber(dto.Number), dto.RoomType, dto.RoomStatus, dto.Capacity, dto.AssignedEquipment, slots);
    }
    public SurgeryRoomDTO ReturnDTO() {
        List<string> slots = [];
        foreach(DaySlots slot in MaintenanceSlots){
            slots.Add(slot.ToString());
        }
        return new SurgeryRoomDTO(Number.Number, RoomType, RoomStatus, Capacity, AssignedEquipment, slots);
    }
}

