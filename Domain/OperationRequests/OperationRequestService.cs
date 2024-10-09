using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.OperationRequests {

    public class OperationRequestService {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _repository;

        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository repository) {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

    }

}