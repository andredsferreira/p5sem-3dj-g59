using Backend.Domain.OperationTypes;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.OperationTypes;

public class OperationTypeRepository : BaseRepository<OperationType, OperationTypeId>, IOperationTypeRepository {

    public OperationTypeRepository(AppDbContext context) : base(context.OperationTypes) {
        
    }

    public OperationTypeRepository(DbSet<OperationType> objs) : base(objs) {
        
    }
}

