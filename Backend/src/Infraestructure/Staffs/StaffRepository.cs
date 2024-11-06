using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Staffs;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Staffs;

public class StaffRepository : BaseRepository<Staff, StaffId>, IStaffRepository {

    public StaffRepository(AppDbContext context) : base(context.Staffs) {

    }

    public async Task<Staff> GetByIdentityUsernameAsync(string identityUsername) {
        return await _objs.FirstOrDefaultAsync(s => s.IdentityUsername == identityUsername);
    }


}