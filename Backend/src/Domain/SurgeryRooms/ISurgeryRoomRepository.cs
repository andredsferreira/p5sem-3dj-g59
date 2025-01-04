using System.Threading.Tasks;
using Backend.Domain.Shared;

namespace Backend.Domain.SurgeryRooms;

public interface ISurgeryRoomRepository : IRepository<SurgeryRoom, SurgeryRoomId> {
    public Task<SurgeryRoom> GetByNumber(RoomNumber roomNumber);
}

