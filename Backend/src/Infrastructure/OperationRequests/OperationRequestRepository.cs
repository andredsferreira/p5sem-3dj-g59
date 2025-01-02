using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.OperationRequests;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.OperationRequests;

public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestId>, IOperationRequestRepository {

    public OperationRequestRepository(AppDbContext context) : base(context.OperationRequests) {
        if (context == null) {
            throw new ArgumentNullException(nameof(context), "AppDbContext cannot be null");
        }

        if (context.OperationRequests == null) {
            throw new ArgumentNullException(nameof(context.OperationRequests), "DbSet<OperationRequest> is null");
        }
    }


}
