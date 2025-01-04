using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.SurgeryRooms;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.SurgeryRooms;

public class SurgeryRoomRepository : BaseRepository<SurgeryRoom, SurgeryRoomId>, ISurgeryRoomRepository {

    #nullable disable

    private readonly AppDbContext _context;

    public SurgeryRoomRepository(AppDbContext context) : base(context.Rooms) {
        _context = context;
    }

    public async Task<SurgeryRoom> GetByNumber(RoomNumber roomNumber)
    {
        return await _context.Rooms.Where(p => p.Number.Equals(roomNumber)).SingleOrDefaultAsync();
    }
}

