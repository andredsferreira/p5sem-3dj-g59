using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Staffs;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Staffs;

public class StaffRepository : BaseRepository<Staff, StaffId>, IStaffRepository {

    #nullable disable

    private readonly AppDbContext _context;

    public StaffRepository(AppDbContext context) : base(context.Staffs) {
        _context = context;
    }

    public Staff GetByIdentityUsername(string identityUsername) {
        return _context.Staffs.Where(p => p.IdentityUsername.Equals(identityUsername)).SingleOrDefault();
    }

    public Staff GetByLicenseNumber(LicenseNumber license) {
        return _context.Staffs.Where(p => p.LicenseNumber.Equals(license)).SingleOrDefault();  
    }


}