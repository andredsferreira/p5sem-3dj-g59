using DDDSample1.Domain.OperationTypes;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.OperationTypes;

public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeId>, IOperationTypeRepository {

    public OperationTypeRepository(DbSet<OperationType> objs) : base(objs) {
        
    }
}

