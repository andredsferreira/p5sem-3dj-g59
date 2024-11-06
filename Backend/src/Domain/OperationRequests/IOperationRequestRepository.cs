using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.Shared;

namespace Backend.Domain.OperationRequests;

public interface IOperationRequestRepository : IRepository<OperationRequest, OperationRequestId> {

}

