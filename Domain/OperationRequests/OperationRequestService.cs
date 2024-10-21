using System;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using Microsoft.AspNetCore.Routing;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IOperationRequestRepository _repository;

    public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository repository) {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<OperationRequestDTO> CreateOperationRequest(OperationRequestDTO dto) {
        var operationRequest = OperationRequest.CreateFromDTO(dto);
        await _repository.AddAsync(operationRequest);
        await _unitOfWork.CommitAsync();
        return dto;
    }

}
