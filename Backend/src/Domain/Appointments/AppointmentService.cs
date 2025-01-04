using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Auth;
using Backend.Domain.OperationRequests;
using Backend.Domain.OperationTypes;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Domain.Shared.Exceptions;
using Backend.Domain.Staffs;
using Backend.Domain.SurgeryRooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domain.Appointments;

public class AppointmentService {

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IAppointmentRepository _appointmentRepository;

    private readonly IOperationRequestRepository _operationRequestRepository;

    private readonly ISurgeryRoomRepository _surgeryRoomRepository;


    public AppointmentService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository ,IOperationRequestRepository operationRequestRepository, ISurgeryRoomRepository surgeryRoomRepository) {
        _httpContextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
        _appointmentRepository = appointmentRepository;
        _operationRequestRepository = operationRequestRepository;
        _surgeryRoomRepository = surgeryRoomRepository;
    }

    public virtual async Task<Guid> CreateAppointment (CreateAppointmentDTO dto) {

        var fetchedUsername = GetClaimFromJwt("username");
        if (fetchedUsername == null) {
            throw new EmptyUserNameException("Your accessing with an empty username");
        }

        var fetchedOperationRequest = await _operationRequestRepository.
            GetByIdAsync(new OperationRequestId(dto.operationRequestId));
        if (fetchedOperationRequest == null) {
            throw new InvalidOperationTypeException("That operation request is not valid.");
        }

        var fetchedSurgeryRoom = await _surgeryRoomRepository.GetByNumber(new RoomNumber(dto.roomNumber));
        if (fetchedSurgeryRoom == null) {
            throw new Exception("That surgery room is not valid");
        }

        var appointment = new Appointment {
            Id = new AppointmentId(Guid.NewGuid()),
            OperationRequestId = fetchedOperationRequest.Id,
            SurgeryRoomId = fetchedSurgeryRoom.Id,
            DateTime = dto.dateTime,
            AppointmentStatus = (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus),
                dto.appointmentStatus, true)
        };

        await _appointmentRepository.AddAsync(appointment);
        await _unitOfWork.CommitAsync();

        return appointment.Id.AsGuid();

    }

    public virtual async Task<Guid> UpdateAppointment(UpdateAppointmentDTO dto) {

        var appointment = await _appointmentRepository.GetByIdAsync(new AppointmentId(dto.updatedId));
        if (appointment == null) {
            throw new Exception("The appointment you are trying to update does not exist!");
        }

        var fetchedUsername = GetClaimFromJwt("username");
        if (fetchedUsername == null) {
            throw new EmptyUserNameException("Your accessing with an empty username");
        }
        if (dto.roomNumber > 0) {
            var fetchedSurgeryRoom = await _surgeryRoomRepository.GetByNumber(new RoomNumber(dto.roomNumber));
            if (fetchedSurgeryRoom == null) {
            throw new Exception("That surgery room is not valid");
            }
            appointment.SurgeryRoomId = fetchedSurgeryRoom.Id;
        }
        if (dto.dateTime.HasValue) {
            appointment.DateTime = dto.dateTime.Value;
        }
        if (!string.IsNullOrEmpty(dto.appointmentStatus)) {
            appointment.AppointmentStatus = (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), dto.appointmentStatus, true);
        }

        this._appointmentRepository.Update(appointment);
        await this._unitOfWork.CommitAsync();

        return appointment.Id.AsGuid();

    }

    /*public virtual async Task<Guid> DeleteOperationRequest(Guid id) {

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

    }*/

    /*public virtual async Task<List<ListOperationRequestDTO>> ListOperationRequests() {

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
    }*/

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
