using System;
using Backend.Domain.Shared;
using Backend.Domain.Staffs;
using Backend.Domain.SurgeryRooms;

namespace Backend.Domain.Slots;

public class Slot : Entity<SlotId> {
    public SurgeryRoomId surgeryRoomId;
    public StaffId staffId;
    public DateOnly day;
    public TimeOnly begin, end;
    public Slot(SurgeryRoomId surgeryRoomId, DateOnly day, TimeOnly begin, TimeOnly end){
        generalConstructor(day, begin, end);
        this.surgeryRoomId = surgeryRoomId;
    }
    public Slot(StaffId staffId, DateOnly day, TimeOnly begin, TimeOnly end){
        generalConstructor(day, begin, end);
        this.staffId = staffId;
    }
    private void generalConstructor(DateOnly day, TimeOnly begin, TimeOnly end){
        if(begin.CompareTo(end) > 0) throw new Exception("End time cannot be before Start time");
        Id = new SlotId(Guid.NewGuid());
        this.day = day;
        this.begin = begin;
        this.end = end;
    }
}