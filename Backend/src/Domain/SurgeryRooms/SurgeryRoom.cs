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
    //public List<Slot> MaintenanceSlots { get; set; }
    public List<string> MaintenanceSlots {get; set;}
    private SurgeryRoom(){}

    public SurgeryRoom(RoomNumber Number, RoomType RoomType, RoomStatus RoomStatus, int Capacity, List<string> AssignedEquipment, /*List<Slot>*/ List<string> MaintenanceSlots) {
        Id = new SurgeryRoomId(Guid.NewGuid());
        this.Number = Number;
        this.RoomType = RoomType;
        this.RoomStatus = RoomStatus;
        this.Capacity = Capacity;
        this.AssignedEquipment = AssignedEquipment;
        this.MaintenanceSlots = MaintenanceSlots;
    }

    //public static SurgeryRoom CreateFromDTO(SurgeryRoomDTO dto) {
    //    List<Slot> slots = [];
    //    for(int i = 0; i < dto.SlotDays.Count; i++)
    //        slots.Add(new Slot(Id, dto.SlotDays[i], dto.SlotBeginnings[i], dto.SlotEndings[i]));
    //    return new SurgeryRoom(new RoomNumber(dto.Number), dto.RoomType, dto.RoomStatus, dto.Capacity, dto.AssignedEquipment, slots);
    //}
    public static SurgeryRoom CreateFromDTO(SurgeryRoomDTO dto) {
        return new SurgeryRoom(new RoomNumber(dto.Number), dto.RoomType, dto.RoomStatus, dto.Capacity, dto.AssignedEquipment, dto.Slots);
    }
    //public SurgeryRoomDTO ReturnDTO() {
    //    List<DateOnly> days = [];
    //    List<TimeOnly> beginnings = [], endings = [];
    //    foreach(Slot slot in MaintenanceSlots){
    //        days.Add(slot.day);
    //        beginnings.Add(slot.begin);
    //        endings.Add(slot.end);
    //    }
    //    return new SurgeryRoomDTO(Number.Number, RoomType, RoomStatus, Capacity, AssignedEquipment, days, beginnings, endings);
    //}
    public SurgeryRoomDTO ReturnDTO() {
        return new SurgeryRoomDTO(Number.Number, RoomType, RoomStatus, Capacity, AssignedEquipment, MaintenanceSlots);
    }
}

