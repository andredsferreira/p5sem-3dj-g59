using System;
using System.Collections.Generic;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Domain.SurgeryRooms;

public class SurgeryRoom : Entity<RoomNumber>, IAggregateRoot {
    
    public RoomType RoomType {get;set;}
    public RoomStatus RoomStatus {get;set;}
    public int Capacity {get;set;}
    public List<string> AssignedEquipment {get;set;}
    public List<string> MaintenanceSlots {get;set;}

    public SurgeryRoom(RoomType RoomType, RoomStatus RoomStatus, int Capacity, List<string> AssignedEquipment, List<string> MaintenanceSlots){
        Id = new RoomNumber(Guid.NewGuid());
        this.RoomType = RoomType;
        this.RoomStatus = RoomStatus;
        this.Capacity = Capacity;
        this.AssignedEquipment = AssignedEquipment;
        this.MaintenanceSlots = MaintenanceSlots;
    }
    
    public static SurgeryRoom CreateFromDTO(SurgeryRoomDTO dto){
        return new SurgeryRoom(dto.RoomType, dto.RoomStatus, dto.Capacity, dto.AssignedEquipment, dto.MaintenanceSlots);
    }
    public SurgeryRoomDTO ReturnDTO(){
        return new SurgeryRoomDTO(RoomType,RoomStatus,Capacity,AssignedEquipment,MaintenanceSlots);
    }
}

