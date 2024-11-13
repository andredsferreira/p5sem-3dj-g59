using Backend.Domain.Shared;

namespace Backend.Domain.SurgeryRooms;

public interface ISurgeryRoomRepository : IRepository<SurgeryRoom, SurgeryRoomId> {
    public SurgeryRoom GetByNumber(RoomNumber roomNumber);
}

