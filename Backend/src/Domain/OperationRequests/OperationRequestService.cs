using System;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Infrastructure.OperationTypes;
using Microsoft.AspNetCore.Routing;

namespace DDDSample1.Domain.OperationRequests;

public class OperationRequestService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly IOperationRequestRepository _operationRequestRepository;

    private readonly IPatientRepository _patientRepository;

    private readonly IStaffRepository _staffRepository;

    private readonly IOperationTypeRepository _operationTypeRepository;

    public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository, IPatientRepository patientRepository, IStaffRepository staffRepository, IOperationTypeRepository operationTypeRepository) {
        _unitOfWork = unitOfWork;
        _operationRequestRepository = operationRequestRepository;
        _patientRepository = patientRepository;
        _staffRepository = staffRepository;
        _operationTypeRepository = operationTypeRepository;

    }

    public async Task<OperationRequestDTO> CreateOperationRequest(OperationRequestDTO dto) {
        var patient = await _patientRepository.GetByIdAsync(new PatientId(dto.patientId));
        var staff = await _staffRepository.GetByIdAsync(new StaffId(dto.patientId));
        var operationType = await _operationTypeRepository.GetByIdAsync(new OperationTypeId(dto.operationTypeId));

        var operationRequest = OperationRequest.CreateFromDTO(dto);

        operationRequest.patient = patient;
        operationRequest.staff = staff;
        operationRequest.operationType = operationType;

        await _operationRequestRepository.AddAsync(operationRequest);
        await _unitOfWork.CommitAsync();

        return dto;
    }

    

}
