using System.Collections.Generic;
using Backend.Domain.Shared;
using Backend.Domain.Staffs;
using Backend.Domain.SurgeryRooms;

namespace Backend.Domain.Slots;

public interface ISlotRepository : IRepository<Slot, SlotId> {

    public List<Slot> GetSlotsByRoomId(SurgeryRoomId roomId);

    public List<Slot> GetSlotsByStaffId(StaffId roomId);
    
}
