using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Http;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.OperationTypes;
using System.Security.Claims;
using System.Net.Mail;
using DDDSample1.Domain.Auth;
using System.Linq;

public class OperationRequestServiceTests {

    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly Mock<IOperationRequestRepository> _mockOperationRequestRepository;

    private readonly Mock<IPatientRepository> _mockPatientRepository;

    private readonly Mock<IStaffRepository> _mockStaffRepository;

    private readonly Mock<IOperationTypeRepository> _mockOperationTypeRepository;

    private readonly OperationRequestService _service;

    public OperationRequestServiceTests() {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockOperationRequestRepository = new Mock<IOperationRequestRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockPatientRepository = new Mock<IPatientRepository>();
        _mockStaffRepository = new Mock<IStaffRepository>();
        _mockOperationTypeRepository = new Mock<IOperationTypeRepository>();

        _service = new OperationRequestService(
            _mockHttpContextAccessor.Object,
            _mockUnitOfWork.Object,
            _mockOperationRequestRepository.Object,
            _mockPatientRepository.Object,
            _mockStaffRepository.Object,
            _mockOperationTypeRepository.Object);
    }

    [Fact]
    public async Task testCreation() {
        var dto = new OperationRequestDTO { patientId = Guid.NewGuid(), operationTypeId = Guid.NewGuid() };
        var patient = new Patient(new MedicalRecordNumber("202410000001"), new DateOnly(2001, 10, 21), new MailAddress("patientA@hospital.com"), new PhoneNumber("910555111"), Gender.Male, new FullName("João Camião"), new List<Allergy>());
        var staff = new Staff(HospitalRoles.Doctor, "doctor");
        var operationType = new OperationType(new OperationName("ACL Reconstruction"));

        _mockPatientRepository.Setup(p => p.GetByIdAsync(It.IsAny<PatientId>())).ReturnsAsync(patient);
        _mockStaffRepository.Setup(s => s.GetByIdentityUsernameAsync(It.IsAny<string>())).ReturnsAsync(staff);
        _mockOperationTypeRepository.Setup(o => o.GetByIdAsync(It.IsAny<OperationTypeId>())).ReturnsAsync(operationType);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Name, "doctor")
        }, "mock"));

        _mockHttpContextAccessor.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);

        var result = await _service.CreateOperationRequest(dto);

        Assert.NotNull(result);
        _mockOperationRequestRepository.Verify(r => r.AddAsync(It.IsAny<OperationRequest>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task testUpdate() {
        var operationRequestId = new OperationRequestId(Guid.NewGuid());
        var operationRequest = new OperationRequest(operationRequestId) {
            staff = new Staff(HospitalRoles.Doctor, "doctor")
        };

        var updatedDto = new UpdatedOperationRequestDTO(operationRequestId.AsGuid(), "Urgent", DateTime.Now);

        _mockOperationRequestRepository
            .Setup(r => r.GetByIdAsync(It.Is<OperationRequestId>(id => id == operationRequestId)))
            .ReturnsAsync(operationRequest);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "doctor") }, "mock"));
        _mockHttpContextAccessor.Setup(h => h.HttpContext.User).Returns(claimsPrincipal);

        var result = await _service.UpdateOperationRequest(updatedDto);

        // Assert
        Assert.NotNull(result);
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task testDelete() {
        var operationRequestId = new OperationRequestId(Guid.NewGuid());
        var operationRequest = new OperationRequest(operationRequestId);
        _mockOperationRequestRepository.Setup(r => r.GetByIdAsync(It.IsAny<OperationRequestId>())).ReturnsAsync(operationRequest);

        var result = await _service.DeleteOperationRequest(operationRequestId.AsGuid());

        Assert.Equal(operationRequestId.AsGuid(), result);
        _mockOperationRequestRepository.Verify(r => r.Remove(It.IsAny<OperationRequest>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task testList() {
        var patientId1 = new PatientId(Guid.NewGuid());
        var patientId2 = new PatientId(Guid.NewGuid());
        var operationTypeId1 = new OperationTypeId(Guid.NewGuid());
        var operationTypeId2 = new OperationTypeId(Guid.NewGuid());
        var staffId1 = new StaffId(Guid.NewGuid());
        var staffId2 = new StaffId(Guid.NewGuid());
        var operationRequests = new List<OperationRequest> {
        new OperationRequest(patientId1, staffId1, operationTypeId1, "High", DateTime.Now, RequestStatus.Pending) {
                patient = new Patient(new MedicalRecordNumber("202410000001"), new DateOnly(1990, 1, 1), new MailAddress("patient1@hospital.com"), new PhoneNumber("123456789"), Gender.Male, new FullName("Patient One"), new List<Allergy>()),
                staff = new Staff(HospitalRoles.Doctor, "doctor1"),
            },
        new OperationRequest(patientId2, staffId2, operationTypeId2, "Low", DateTime.Now.AddDays(1), RequestStatus.Pending) {
                patient = new Patient(new MedicalRecordNumber("202410000002"), new DateOnly(1985, 5, 5), new MailAddress("patient2@hospital.com"), new PhoneNumber("987654321"), Gender.Female, new FullName("Patient Two"), new List<Allergy>()),
                staff = new Staff(HospitalRoles.Doctor, "doctor2"),
            }
        };
        _mockOperationRequestRepository
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(operationRequests);

        var resultDTOs = await _service.ListOperationRequests();

        Assert.NotNull(resultDTOs);
        Assert.Equal(operationRequests.Count, resultDTOs.Count);
        Assert.All(resultDTOs, dto =>
            Assert.Contains(operationRequests.Select(or => OperationRequest.returnDTO(or)), expectedDto =>
                expectedDto.patientId == dto.patientId &&
                expectedDto.operationTypeId == dto.operationTypeId &&
                expectedDto.priority == dto.priority &&
                expectedDto.dateTime == dto.dateTime &&
                expectedDto.requestStatus == dto.requestStatus));
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once); // Ensure commit was called
    }

}
