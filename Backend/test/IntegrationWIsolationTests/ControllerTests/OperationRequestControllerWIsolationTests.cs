using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Backend.Controllers;
using Backend.Domain.Auth;
using Backend.Domain.OperationRequests;
using Backend.Domain.OperationTypes;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Domain.Shared.Exceptions;
using Backend.Domain.Staffs;
using Backend.Infrastructure;
using Backend.Infrastructure.OperationRequests;
using Backend.Infrastructure.Patients;
using Backend.Infrastructure.Staffs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Backend.ControllerTests {
    public class OperationRequestControllerWIsolationTests {

        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private readonly Mock<IOperationRequestRepository> _mockOperationRequestRepository;

        private readonly Mock<IPatientRepository> _mockPatientRepository;
        
        private readonly OperationRequestController _controller;

        private readonly Mock<IStaffRepository> _mockStaffRepository;

        private readonly Mock<IOperationTypeRepository> _mockOperationTypeRepository;

        private readonly OperationRequestService service;

        public OperationRequestControllerWIsolationTests() {
           _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
           _mockUnitOfWork = new Mock<IUnitOfWork>();
           _mockOperationRequestRepository = new Mock<IOperationRequestRepository>();
           _mockPatientRepository = new Mock<IPatientRepository>();
           _mockStaffRepository = new Mock<IStaffRepository>();
           _mockOperationTypeRepository = new Mock<IOperationTypeRepository>();

           service = new OperationRequestService(
               _mockHttpContextAccessor.Object,
               _mockUnitOfWork.Object,
               _mockOperationRequestRepository.Object,
               _mockPatientRepository.Object,
               _mockStaffRepository.Object,
               _mockOperationTypeRepository.Object);

           _controller = new OperationRequestController(service);
        }
        private Patient SeedPatient(){
            return new Patient(new MedicalRecordNumber("202410000001"), DateOnly.Parse("2004-07-10"),new MailAddress("teste@gmail.com"),new PhoneNumber("987876765"),Gender.Male, new FullName("John One Two Doe"), [new Allergy("Dogs"), new Allergy("Cats")]);
        }
        private OperationRequest SeedOperationRequest(Staff staff, Patient patient, OperationType operationType){
            return new OperationRequest {
                Id = new OperationRequestId(Guid.NewGuid()),
                staffId = staff.Id,
                patientId = patient.Id,
                operationTypeId = operationType.Id,
                priority = OperationRequestPriority.Elective,
                dateTime = DateTime.Now,
                requestStatus = OperationRequestStatus.Pending
            };
        }
        private Staff SeedStaff(){
            return new Staff(HospitalRoles.Doctor, "doctor", new MailAddress("doctor@example.com"), new PhoneNumber("123456789"), new FullName("Tiago"), new LicenseNumber("12345"));
        }
        private OperationType SeedOperationType(){
            return new OperationType(new OperationName("ACL Reconstruction"));
        }
        [Fact]
        public async Task TestCreation_Success() {
            //Arrange
            Staff staff = SeedStaff();
            Patient patient = SeedPatient();
            OperationType operationType = SeedOperationType();

            var dto = new CreateOperationRequestDTO{
                medicalRecordNumber= "202410000001",
                operationTypeId= Guid.NewGuid(),
                priority= "Urgent",
                dateTime= DateTime.Now,
                requestStatus= "Pending"
            };

            _mockHttpContextAccessor.Setup(HttpContextAccessor => HttpContextAccessor.HttpContext.User.FindFirst(It.IsAny<string>()))
                .Returns(new System.Security.Claims.Claim("type", "value"));

            _mockStaffRepository.Setup(StaffRepository => StaffRepository.GetByIdentityUsername(It.IsAny<string>()))
                .Returns(staff);

            _mockPatientRepository.Setup(PatientRepository => PatientRepository.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
                .ReturnsAsync(patient);

            _mockOperationTypeRepository.Setup(OperationTypeRepository => OperationTypeRepository.GetByIdAsync(It.IsAny<OperationTypeId>()))
                .ReturnsAsync(operationType);

            _mockOperationRequestRepository.Setup(OperationRequestRepository => OperationRequestRepository.AddAsync(It.IsAny<OperationRequest>()))
                .Returns(Task.FromResult(SeedOperationRequest(staff, patient, operationType)));

            _mockUnitOfWork.Setup(UnitOfWork => UnitOfWork.CommitAsync()).ReturnsAsync(2);

            //Act
            var result = await _controller.CreateOperationRequest(dto);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestCreation_PatientNotFoundException() {
            //Arrange
            var dto = new CreateOperationRequestDTO{
                medicalRecordNumber= "202410000001",
                operationTypeId= Guid.NewGuid(),
                priority= "Urgent",
                dateTime= DateTime.Now,
                requestStatus= "Pending"
            };

            _mockHttpContextAccessor.Setup(HttpContextAccessor => HttpContextAccessor.HttpContext.User.FindFirst(It.IsAny<string>()))
                .Returns(new System.Security.Claims.Claim("type", "value"));

            _mockStaffRepository.Setup(StaffRepository => StaffRepository.GetByIdentityUsername(It.IsAny<string>()))
                .Returns(new Staff(HospitalRoles.Doctor, "doctor", new MailAddress("doctor@example.com"), new PhoneNumber("123456789"), new FullName("Tiago"), new LicenseNumber("12345")));

            _mockPatientRepository.Setup(PatientRepository => PatientRepository.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
                .ReturnsAsync((Patient)null);

            //Act
            var result = await _controller.CreateOperationRequest(dto);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ error = The patient you provided does not exist! }", badRequestResult.Value.ToString());
        }


        [Fact]
        public async Task TestUpdate_Success() {
            //Arrange
            Staff staff = SeedStaff();
            Patient patient = SeedPatient();
            OperationType operationType = SeedOperationType(); 
            OperationRequest opreq = SeedOperationRequest(staff, patient, operationType);           
            var updatedDto = new UpdateOperationRequestDTO(
                opreq.Id.AsGuid(),
                "Elective",
                DateTime.Now
            );

            _mockOperationRequestRepository.Setup(OperationRequestRepository => OperationRequestRepository.GetByIdAsync(It.IsAny<OperationRequestId>()))
                .ReturnsAsync(opreq);

            _mockHttpContextAccessor.Setup(HttpContextAccessor => HttpContextAccessor.HttpContext.User.FindFirst(HospitalClaims.Username))
                .Returns(new System.Security.Claims.Claim(HospitalClaims.Username, staff.IdentityUsername));

            _mockStaffRepository.Setup(StaffRepository => StaffRepository.GetByIdAsync(It.IsAny<StaffId>()))
                .ReturnsAsync(staff);

            _mockOperationRequestRepository.Setup(OperationRequestRepository => OperationRequestRepository.AddAsync(It.IsAny<OperationRequest>()))
                .Returns(Task.FromResult(opreq));

            _mockUnitOfWork.Setup(UnitOfWork => UnitOfWork.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await _controller.UpdateOperationRequest(updatedDto);
        }

        [Fact]
        public async Task TestDelete_Success() {
            //Arrange
            Staff staff = SeedStaff();
            Patient patient = SeedPatient();
            OperationType operationType = SeedOperationType(); 
            OperationRequest opreq = SeedOperationRequest(staff, patient, operationType);

            _mockOperationRequestRepository.Setup(OperationRequestRepository => OperationRequestRepository.GetByIdAsync(It.IsAny<OperationRequestId>()))
                .ReturnsAsync(opreq);

            _mockHttpContextAccessor.Setup(HttpContextAccessor => HttpContextAccessor.HttpContext.User.FindFirst(HospitalClaims.Username))
                .Returns(new System.Security.Claims.Claim(HospitalClaims.Username, staff.IdentityUsername));

            _mockStaffRepository.Setup(StaffRepository => StaffRepository.GetByIdAsync(It.IsAny<StaffId>()))
                .ReturnsAsync(staff);

            _mockOperationRequestRepository.Setup(OperationRequestRepository => OperationRequestRepository.AddAsync(It.IsAny<OperationRequest>()))
                .Returns(Task.FromResult(opreq));

            _mockUnitOfWork.Setup(UnitOfWork => UnitOfWork.CommitAsync()).ReturnsAsync(1);

            //Act
            var result = await _controller.DeleteOperationRequest(opreq.Id.AsGuid());
        }


        [Fact]
        public async Task TestList_NotFound() {
            //Arrange

            _mockOperationRequestRepository.Setup(OperationRequestRepository => OperationRequestRepository.GetAllAsync())
                .ReturnsAsync([]); 

            _mockPatientRepository.Setup(PatientRepository => PatientRepository.GetByIdAsync(It.IsAny<PatientId>()))
                .ReturnsAsync(SeedPatient()); 

            _mockOperationTypeRepository.Setup(OperationTypeRepository => OperationTypeRepository.GetByIdAsync(It.IsAny<OperationTypeId>()))
                .ReturnsAsync(SeedOperationType()); 

            _mockStaffRepository.Setup(StaffRepository => StaffRepository.GetByIdAsync(It.IsAny<StaffId>()))
                .ReturnsAsync(SeedStaff()); 

            _mockUnitOfWork.Setup(UnitOfWork => UnitOfWork.CommitAsync()).ReturnsAsync(2);

            //Act
            var result = await _controller.ListOperationRequests();

            //Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
        