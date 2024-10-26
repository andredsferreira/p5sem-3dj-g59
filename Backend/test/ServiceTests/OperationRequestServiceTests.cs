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

        // Act
        var result = await _service.CreateOperationRequest(dto);

        // Assert
        Assert.NotNull(result);
        _mockOperationRequestRepository.Verify(r => r.AddAsync(It.IsAny<OperationRequest>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    //[Fact]
    //public async Task testUpdate() {
    //    throw new NotImplementedException();
    //}

    //[Fact]
    //public async Task testDelete() {
    //    throw new NotImplementedException();
    //}

    //[Fact]
    //public async Task testList() {
    //    throw new NotImplementedException();
    //}

}
