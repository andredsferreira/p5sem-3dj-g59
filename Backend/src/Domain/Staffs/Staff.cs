using System;
using System.Collections;
using System.Collections.Generic;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class Staff : Entity<StaffId>, IAggregateRoot {

    public string staffRole { get; set; }

    public string identityUsername { get; }

    public ICollection<OperationRequest> OperationRequests { get; set; } = new List<OperationRequest>();

    public Staff() {
        this.Id = new StaffId(Guid.NewGuid());
    }

    public Staff(string staffRole, string username) {
        this.Id = new StaffId(Guid.NewGuid());
        this.staffRole = staffRole;
        this.identityUsername = username;
    }

    public static Staff createFromDTO(StaffDTO dto) {
        throw new NotImplementedException();
    }

    public static StaffDTO returnDTO(Staff staff) {
        throw new NotImplementedException();
    }

}

