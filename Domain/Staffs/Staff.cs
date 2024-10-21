using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

public class Staff : Entity<StaffId>, IAggregateRoot {

    public static Staff createFromDTO(StaffDTO dto) {
        throw new NotImplementedException();
    }
    
}

