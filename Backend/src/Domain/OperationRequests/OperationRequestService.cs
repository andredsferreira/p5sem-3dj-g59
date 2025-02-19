using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Auth;
using Backend.Domain.OperationTypes;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Domain.Shared.Exceptions;
using Backend.Domain.Staffs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domain.OperationRequests;

public class OperationRequestService {

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IOperationRequestRepository _operationRequestRepository;

    private readonly IPatientRepository _patientRepository;

    private readonly IStaffRepository _staffRepository;

    private readonly IOperationTypeRepository _operationTypeRepository;

    public OperationRequestService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository, IPatientRepository patientRepository, IStaffRepository staffRepository, IOperationTypeRepository operationTypeRepository) {
        _httpContextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
        _operationRequestRepository = operationRequestRepository;
        _patientRepository = patientRepository;
        _staffRepository = staffRepository;
        _operationTypeRepository = operationTypeRepository;
    }

    public virtual async Task<Guid> CreateOperationRequest(CreateOperationRequestDTO dto) {

        var fetchedUsername = GetClaimFromJwt("username");
        if (fetchedUsername == null) {
            throw new EmptyUserNameException("Your accessing with an empty username");
        }

        var fetchedStaff = _staffRepository.GetByIdentityUsername(fetchedUsername);
        if (fetchedStaff == null) {
            throw new StaffNotRegisteredException("Your are not registered in the system.");
        }

        var fetchedPatient = await _patientRepository.GetPatientByRecordNumber(new MedicalRecordNumber(dto.medicalRecordNumber));
        if (fetchedPatient == null) {
            throw new PatientNotFoundException("The patient you provided does not exist!");
        }

        var fetchedOperationType = await _operationTypeRepository.
            GetByIdAsync(new OperationTypeId(dto.operationTypeId));
        if (fetchedOperationType == null) {
            throw new InvalidOperationTypeException("That operation type is not valid.");
        }

        var operationRequest = new OperationRequest {
            Id = new OperationRequestId(Guid.NewGuid()),
            staffId = fetchedStaff.Id,
            patientId = fetchedPatient.Id,
            operationTypeId = fetchedOperationType.Id,
            priority = (OperationRequestPriority)Enum.Parse(typeof(OperationRequestPriority),
                dto.priority, true),
            dateTime = dto.dateTime,
            requestStatus = (OperationRequestStatus)Enum.Parse(typeof(OperationRequestStatus),
                dto.requestStatus, true)
        };

        await _operationRequestRepository.AddAsync(operationRequest);
        await _unitOfWork.CommitAsync();

        return operationRequest.Id.AsGuid();

    }

    public virtual async Task<Guid> UpdateOperationRequest(UpdateOperationRequestDTO dto) {

        var operationRequest = await _operationRequestRepository.GetByIdAsync(new OperationRequestId(dto.updatedId));
        if (operationRequest == null) {
            throw new OperationRequestNotFoundException("The operation request you are trying to update does not exist!");
        }

        var fetchedUsername = GetClaimFromJwt("username");
        var operationRequestDoctor = await _staffRepository.GetByIdAsync(operationRequest.staffId);
        
        var doctorUsername = operationRequestDoctor.IdentityUsername;
        if (doctorUsername != fetchedUsername) {
            throw new InvalidOperationRequestException("The operation request you are trying to update is associated with another doctor");
        }

        operationRequest.dateTime = dto.dateTime;
        operationRequest.priority = (OperationRequestPriority)Enum
            .Parse(typeof(OperationRequestPriority), dto.priority, true);

        await _unitOfWork.CommitAsync();

        return operationRequest.Id.AsGuid();

    }

    public virtual async Task<Guid> DeleteOperationRequest(Guid id) {

        var operationRequest = await _operationRequestRepository.GetByIdAsync(new OperationRequestId(id));
        if (operationRequest == null) {
            throw new OperationRequestNotFoundException("That operation request does not exist");
        }

        var loggedUsername = GetClaimFromJwt("username");
        
        var operationRequestDoctor = await _staffRepository.GetByIdAsync(operationRequest.staffId);
        var doctorUsername = operationRequestDoctor.IdentityUsername;
        if (doctorUsername != loggedUsername) {
            throw new InvalidOperationRequestException("The operation request you are trying to update is associated with another doctor");
        }

        _operationRequestRepository.Remove(operationRequest);
        await _unitOfWork.CommitAsync();

        return id;

    }

    public virtual async Task<List<ListOperationRequestDTO>> ListOperationRequests() {

        var operationRequests = await _operationRequestRepository.GetAllAsync();

        var returnList = new List<ListOperationRequestDTO>();

        foreach (var or in operationRequests) {
            var orPatient = await _patientRepository.GetByIdAsync(or.patientId);
            var orType = await _operationTypeRepository.GetByIdAsync(or.operationTypeId);
            var orStaff = await _staffRepository.GetByIdAsync(or.staffId);
            var listOperationRequestDTO = new ListOperationRequestDTO {
                operationRequestId = or.Id.AsGuid(),
                doctorName = orStaff.FullName.Full,
                medicalRecordNumber = or.patient.MedicalRecordNumber.ToString(),
                patientFullName = orPatient.FullName.Full,
                operationTypeName = orType.name.name,
                priority = or.priority.ToString(),
                dateTime = or.dateTime,
                requestStatus = or.requestStatus.ToString()
            };
            returnList.Add(listOperationRequestDTO);
        }

        await _unitOfWork.CommitAsync();

        return returnList;
    }

    private string GetClaimFromJwt(string claimType) {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) {
            throw new UnauthorizedAccessException("No HTTP context available.");
        }
        var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer ")) {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(token)) {
                var jwtToken = handler.ReadJwtToken(token);
                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);
                return claim?.Value;
            }
        }

        throw new UnauthorizedAccessException("Invalid or missing JWT.");
    }

}
