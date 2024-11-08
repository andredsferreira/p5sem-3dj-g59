using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.DomainLogs;
using Backend.Domain.Staffs;
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

    

    public async Task<StaffDTO> CreateStaff(StaffDTO dto){
        //dto.license = (await GenerateLicense()).ToString();
        var staff = Staff.createFromDTO(dto);
        await _staffRepository.AddAsync(staff);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Staff, LogActionType.Creation, 
            string.Format("Created a new Staff (License Number = {0}, Name = {1}, Email = {2}, PhoneNumber = {3})",
                        staff.LicenseNumber, staff.FullName.Full, staff.Email, staff.PhoneNumber)));

        await this._unitOfWork.CommitAsync();
        
        return dto;
    }

    public StaffDTO GetStafftById(LicenseNumber id) {
        var staff = this._staffRepository.GetByLicenseNumber(id);
        if (staff == null) return null;
        return staff.returnDTO();
    }

    public async virtual Task<StaffDTO> EditStaff(LicenseNumber id, FilterStaffDTO dto) {
        var staff = this._staffRepository.GetByLicenseNumber(id);
        if (staff == null) return null;

        bool warn = false;
        string email = staff.Email.ToString();
        StringBuilder messageBuilder = new(string.Format("Hello {0},<br>This message was sent to warn you that:<br>", staff.FullName.Full)),
            logBuilder = new(string.Format("Edit in Staff {0}: ", id.License));

        if (!string.IsNullOrEmpty(dto.FullName)) {
            logBuilder.Append(string.Format("Full name changed from {0} to {1}, ", staff.FullName.Full, dto.FullName));
            staff.FullName = new FullName(dto.FullName);
        }

        if (!string.IsNullOrEmpty(dto.PhoneNumber)) {
            warn = true;
            logBuilder.Append(string.Format("Phone Number changed from {0} to {1}, ", staff.PhoneNumber, dto.PhoneNumber));
            messageBuilder.Append(string.Format("-The Phone Number associated with your account was changed from {0} to {1}.<br>", staff.PhoneNumber, dto.PhoneNumber));
            staff.PhoneNumber = new PhoneNumber(dto.PhoneNumber);
        }
        if (!string.IsNullOrEmpty(dto.Email)) {
            warn = true;
            logBuilder.Append(string.Format("Email changed from {0} to {1}, ", staff.Email, dto.Email));
            messageBuilder.Append(string.Format("-The Email associated with your account was changed from {0} to {1}.<br>", staff.Email, dto.Email));
            staff.Email = new MailAddress(dto.Email);
        }
        

        this._staffRepository.Update(staff);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Staff, LogActionType.Edit, logBuilder.ToString()));
        await this._unitOfWork.CommitAsync();

        if (warn) {
            messageBuilder.Append("<br>This message was sent automatically. Don't answer it.<br>");
            _messageSender.SendMessage(email, "Some of your data was altered", messageBuilder.ToString());
        }

        return staff.returnDTO();
    }

    public async Task<IEnumerable<StaffDTO>> GetAll() {
        var list = _staffRepository.GetAllAsync();
        return (await list).Select(staff => staff.returnDTO());
    }

    public virtual async Task<StaffDTO> DeleteStaff(LicenseNumber id) {
        var staff = this._staffRepository.GetByLicenseNumber(id);
        if (staff == null) return null;

        this._staffRepository.Remove(staff);
        await this._logRepository.AddAsync(new DomainLog(LogObjectType.Staff, LogActionType.Deletion,
            string.Format("Deleted Staff with License Number = {0}", staff.LicenseNumber.License)));
        await this._unitOfWork.CommitAsync();

        return staff.returnDTO();
    }
}
