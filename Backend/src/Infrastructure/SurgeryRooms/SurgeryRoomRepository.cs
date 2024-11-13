using System.Linq;
using Backend.Domain.SurgeryRooms;
using Backend.Infrastructure.Shared;

namespace Backend.Infrastructure.SurgeryRooms;

public class SurgeryRoomRepository : BaseRepository<SurgeryRoom, SurgeryRoomId>, ISurgeryRoomRepository {

    #nullable disable

    private readonly AppDbContext _context;

    public SurgeryRoomRepository(AppDbContext context) : base(context.Rooms) {
        _context = context;
    }

    public SurgeryRoom GetByNumber(RoomNumber roomNumber)
    {
        return _context.Rooms.Where(p => p.Number.Equals(roomNumber)).SingleOrDefault();
    }
}

