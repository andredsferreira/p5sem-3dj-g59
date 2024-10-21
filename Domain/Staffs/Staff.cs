using System;
using System.Collections;
using System.Collections.Generic;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class Staff : Entity<StaffId>, IAggregateRoot {

    public ICollection<OperationRequest> OperationRequests { get; set; } = new List<OperationRequest>();

    public Staff() {
        this.Id = new StaffId(Guid.NewGuid());
    }

    public static Staff createFromDTO(StaffDTO dto) {
        throw new NotImplementedException();
    }

}

