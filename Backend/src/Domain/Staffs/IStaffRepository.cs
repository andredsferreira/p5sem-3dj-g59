using System.Threading.Tasks;
using Backend.Domain.Shared;

namespace Backend.Domain.Staffs;

public interface IStaffRepository : IRepository<Staff, StaffId> {

    Staff GetByIdentityUsername(string identityUsername);
    Staff GetByLicenseNumber(LicenseNumber license);

}

