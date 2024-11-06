using Backend.Domain.OperationRequests;
using Backend.Infrastructure.Shared;

namespace Backend.Infrastructure.OperationRequests;

public class OperationRequestRepository : BaseRepository<OperationRequest, OperationRequestId>, IOperationRequestRepository {

    public OperationRequestRepository(AppDbContext context) : base(context.OperationRequests) {

    }

}
