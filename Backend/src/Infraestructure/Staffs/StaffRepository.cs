using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Staffs;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Staffs;

public class StaffRepository : BaseRepository<Staff, StaffId>, IStaffRepository {

    public StaffRepository(DDDSample1DbContext context) : base(context.Staffs) {

    }

    public async Task<Staff> GetByIdentityUsernameAsync(string identityUsername) {
        return await _objs.FirstOrDefaultAsync(s => s.IdentityUsername == identityUsername);
    }


}