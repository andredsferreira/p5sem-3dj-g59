using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.DomainLogs;
using Backend.Domain.Shared;
using Backend.Infrastructure.Shared.MessageSender;

namespace Backend.Domain.Staffs;

public class StaffService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IStaffRepository _staffRepository;
    private readonly IDomainLogRepository _logRepository;
    private readonly IMessageSenderService _messageSender;

    public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, IDomainLogRepository logRepository, IMessageSenderService messageSender) {
        _unitOfWork = unitOfWork;
        _staffRepository = staffRepository;
        _logRepository = logRepository;
        _messageSender = messageSender;
    }

    public async Task<Staff> getStaffByIdentityUsername(string identityUsername) {
        return await _staffRepository.GetByIdentityUsernameAsync(identityUsername);
    }

    public async Task<StaffDTO> CreateStaff(StaffDTO dto){
        
        var staff = Staff.createFromDTO(dto);
        await _staffRepository.AddAsync(staff);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Staff, LogActionType.Creation, 
            string.Format("Created a new Staff (License Number = {0}, Name = {1}, Email = {2}, PhoneNumber = {3})",
                        staff.LicenseNumber, staff.FullName.Full, staff.Email, staff.PhoneNumber)));

        await this._unitOfWork.CommitAsync();
        
        return dto;
    }

    public async Task<IEnumerable<StaffDTO>> GetAll() {
        var list = _staffRepository.GetAllAsync();
        return (await list).Select(staff => staff.returnDTO());
    }
}
