using System.Linq;
using DDDSample1.Domain.Staffs;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Staffs;

public class StaffRepository : BaseRepository<Staff, StaffId>, IStaffRepository {

    public StaffRepository(DbSet<Staff> objs) : base(objs) {
        
    }

    public Staff getByIdentityUsername(string identityUsername) {
        return _objs.FirstOrDefault(s => s.identityUsername == identityUsername);
    }
}