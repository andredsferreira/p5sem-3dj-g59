using System;
using System.Collections.Generic;
using System.Net.Mail;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class Staff : Entity<StaffId>, IAggregateRoot {

    public string StaffRole { get; set; }
    

    public string IdentityUsername { get; }
    public MailAddress Email { get; set; } //unique
    public PhoneNumber PhoneNumber { get; set; } //unique
    public FullName FullName { get; set; }
    public LicenseNumber LicenseNumber { get; set; } //unique   

    public ICollection<OperationRequest> OperationRequests { get; set; } = new List<OperationRequest>();

    public Staff() {
        
    }

    public Staff(string staffRole,string identityUsername, MailAddress Email, PhoneNumber PhoneNumber, FullName FullName, LicenseNumber LicenseNumber) {
        this.Id = new StaffId(Guid.NewGuid());
        this.IdentityUsername = identityUsername;
        this.StaffRole = staffRole;
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.FullName = FullName;
        this.LicenseNumber = LicenseNumber;
        
    }

    public static Staff createFromDTO(StaffDTO dto) {
        return new Staff(dto.StaffRole, dto.IdentityUsername, new MailAddress(dto.Email), new PhoneNumber(dto.Phone), new FullName(dto.Name), new LicenseNumber(dto.LicenseNumber));

    }

    public StaffDTO returnDTO() {
        return new StaffDTO(StaffRole, IdentityUsername, Email.ToString(), PhoneNumber.ToString(), FullName.ToString(), LicenseNumber.ToString());
    }


}

