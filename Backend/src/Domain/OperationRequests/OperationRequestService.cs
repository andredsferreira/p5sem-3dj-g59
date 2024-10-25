using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Infrastructure.OperationTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestService {

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IOperationRequestRepository _operationRequestRepository;

    private readonly IPatientRepository _patientRepository;

    private readonly IStaffRepository _staffRepository;

    private readonly IOperationTypeRepository _operationTypeRepository;

    public OperationRequestService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository, IPatientRepository patientRepository, IStaffRepository staffRepository, IOperationTypeRepository operationTypeRepository) {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _operationRequestRepository = operationRequestRepository;
        _patientRepository = patientRepository;
        _staffRepository = staffRepository;
        _operationTypeRepository = operationTypeRepository;
    }

    public async Task<OperationRequestDTO> CreateOperationRequest(OperationRequestDTO dto) {
        var patient = await _patientRepository.GetByIdAsync(new PatientId(dto.patientId));
        if (patient == null) {
            throw new Exception("The patient you provided does not exist!");
        }
        var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        if (username != null) {
            throw new Exception("Your accessing with an empty username");
        }
        var staff = _staffRepository.getByIdentityUsername(username);
        if (staff == null) {
            throw new Exception("Your are not registered in the system.");
        }
        var operationType = await _operationTypeRepository.GetByIdAsync(new OperationTypeId(dto.operationTypeId));
        if (operationType == null) {
            throw new Exception("That operation type is not valid.");
        }
        var operationRequest = OperationRequest.CreateFromDTO(dto);
        // Possivelmente dara erro...
        operationRequest.patient = patient;
        operationRequest.staff = staff;
        operationRequest.operationType = operationType;
        await _operationRequestRepository.AddAsync(operationRequest);
        await _unitOfWork.CommitAsync();
        return dto;
    }

    public async Task<UpdatedOperationRequestDTO> UpdateOperationRequest(UpdatedOperationRequestDTO dto) {
        var operationRequest = await _operationRequestRepository.GetByIdAsync(new OperationRequestId(dto.updatedId));
        if (operationRequest == null) {
            throw new Exception("The operation request you are trying to update does not exist!");
        }
        var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        if (operationRequest.staff.IdentityUsername != username) {
            throw new Exception("The operation request you are trying to update is associated with another doctor");
        }
        operationRequest.dateTime = dto.dateTime;
        operationRequest.priority = dto.priority;
        await _unitOfWork.CommitAsync();
        return dto;
    }

    public async Task<Guid> DeleteOperationRequest(Guid id) {
        var operationRequest = await _operationRequestRepository.GetByIdAsync(new OperationRequestId(id));
        if (operationRequest == null) {
            throw new Exception("That operation request does not exist");
        }
        _operationRequestRepository.Remove(operationRequest);
        await _unitOfWork.CommitAsync();
        return id;
    }

    public async Task<List<OperationRequestDTO>> ListOperationRequests() {
        var operationRequests = await _operationRequestRepository.GetAllAsync();
        var operationRequestsDTO = new List<OperationRequestDTO>();
        foreach (var operationRequest in operationRequests) {
            operationRequestsDTO.Add(OperationRequest.returnDTO(operationRequest));
        }
        await _unitOfWork.CommitAsync();
        return operationRequestsDTO;
    }

}
