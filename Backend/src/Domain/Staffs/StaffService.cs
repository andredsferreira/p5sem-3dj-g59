using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class StaffService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IStaffRepository _staffRepository;

    public Staff getStaffByIdentityUsername(string identityUsername) {
        return _staffRepository.getByIdentityUsername(identityUsername);
    }
    

}