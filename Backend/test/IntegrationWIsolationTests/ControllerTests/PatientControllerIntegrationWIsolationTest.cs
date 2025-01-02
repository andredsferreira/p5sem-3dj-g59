using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Domain.Patients;
using Backend.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System;
using Backend.Domain.Shared;
using Backend.Infrastructure.Shared.MessageSender;
using Backend.Domain.DomainLogs;
using System.Collections.Generic;
using System.Net.Mail;

namespace Backend.UnitTests.ControllerTests;

public class PatientControllerIntegrationWIsolationTests
{
    private readonly Mock<IPatientRepository> _repo;
    private readonly Mock<IDomainLogRepository> _logrepo;
    private readonly Mock<IUnitOfWork> _unit;
    private readonly Mock<UserManager<IdentityUser>> _manager;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly PatientService service;
    private readonly PatientController _controller;

    public PatientControllerIntegrationWIsolationTests()
    {
        _unit = new Mock<IUnitOfWork>();
        _repo = new Mock<IPatientRepository>();
        _logrepo = new Mock<IDomainLogRepository>();
        _manager = new Mock<UserManager<IdentityUser>>(
            Mock.Of<IUserStore<IdentityUser>>(),
            null, null, null, null, null, null, null, null);

        _mockMessageSender = new Mock<IMessageSenderService>();

        service = new PatientService(_unit.Object, _repo.Object, _logrepo.Object, _mockMessageSender.Object);
        _controller = new PatientController(service, _manager.Object);
    }

    private Patient SeedPatient(){
        return new Patient(new MedicalRecordNumber("202410000001"), DateOnly.Parse("2004-07-10"),new MailAddress("teste@gmail.com"),new PhoneNumber("987876765"),Gender.Male, new FullName("John One Two Doe"), [new Allergy("Dogs"), new Allergy("Cats")]);
    }
    private PatientDTO SeedPatientDTO1(){
        return new PatientDTO(string.Format("{0}{1:D2}000001", DateTime.Today.Year, DateTime.Today.Month), DateOnly.Parse("2004-07-10"),"diogo10072004@gmail.com","987876765","Male","John One Two Doe", "Dogs, Cats");
    }
    private PatientDTO SeedPatientDTO2(){
        return new PatientDTO(string.Format("{0}{1:D2}000002", DateTime.Today.Year, DateTime.Today.Month), DateOnly.Parse("2004-07-14"),"teste2@gmail.com","922114411","Female","Jane One Two Doe", "Cats");
    }
    private PatientDTO SeedPatientDTO3(){
        return new PatientDTO(string.Format("{0}{1:D2}000001", DateTime.Today.Year, DateTime.Today.Month), DateOnly.Parse("2004-07-10"),"novo@gmail.com","912834756","Male","Joao One Two Doe", "Dogs");
    }
    private FilterPatientDTO SeedFilterPatientDTO(){
        return new FilterPatientDTO{Email = "novo@gmail.com"};
    }
    private FilterPatientDTO SeedFilterPatientDTOWithSensitiveData(){
        return new FilterPatientDTO{Email = "novo@gmail.com", PhoneNumber = "912834756"};
    }
    private FilterPatientDTO SeedFilterPatientDTOWithOnlyGender(){
        return new FilterPatientDTO{Gender = "Male"};
    }

    [Fact]
    public async Task CreatePatient_ReturnsCreatedAtAction_WithPatientDTO()
    {
        // Arrange
        var patientDto = SeedPatientDTO1();
        var existingPatient = SeedPatient(); 

        _repo.Setup(repo => repo.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync((MedicalRecordNumber id) =>
                id.Equals(new MedicalRecordNumber("202410000001")) ? existingPatient : null);

        _repo.Setup(repo => repo.AddAsync(It.IsAny<Patient>()))
            .ReturnsAsync(SeedPatient());

        _repo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO1()), Patient.createFromDTO(SeedPatientDTO2())}));

        _unit.Setup(u => u.CommitAsync()).ReturnsAsync(2);

        // Act
        var result = await _controller.CreatePatient(patientDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);

        Assert.Equal(patientDto.MedicalRecordNumber, returnValue.MedicalRecordNumber);
    }


    [Fact]
    public async Task EditPatient_ReturnsNotFoundWhenGettingNull() {

        var existingPatient = SeedPatient(); 

        _repo.Setup(repo => repo.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync((MedicalRecordNumber id) =>
                id.Equals(new MedicalRecordNumber("202410000001")) ? existingPatient : null);

        // Act
        var result = await _controller.EditPatient("202410000001", SeedFilterPatientDTO());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task EditPatient_ReturnsOkAndDTOWhenGettingDTO() {
        // Arrange
        var existingPatient = SeedPatient(); 

        var patientDto = SeedPatientDTO1();

        _repo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync(Patient.createFromDTO(patientDto));

        // Act
        var result = await _controller.EditPatient("202410000001", SeedFilterPatientDTO());

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<PatientDTO>(actionResult.Value);

        Assert.NotEqual(patientDto.Email, ((PatientDTO)actionResult.Value).Email); //Email has changed
    }

    [Fact]
    public async Task DeletePatient_ReturnsNotFoundWhenGettingNull() {

        // Arrange
        _repo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync((Patient)null);

        // Act
        var result = await _controller.DeletePatient("202410000004"); //Random MedicalRecordNumber

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeletePatient_ReturnsOkAndDTOWhenGettingDTO() {
        // Arrange
        var patientDto = SeedPatientDTO1();
        _repo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync(Patient.createFromDTO(patientDto));

        // Act
        var result = await _controller.DeletePatient(patientDto.MedicalRecordNumber);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);
    }

    [Fact]
    public async Task SearchPatient_ReturnsOkAndListDTOWhenGettingList() {

        _repo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO3()), Patient.createFromDTO(SeedPatientDTO2())}));
        
        // Act
        var result = await _controller.SearchAndFilterPatients(SeedFilterPatientDTOWithSensitiveData());

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<PatientDTO>>(actionResult.Value);
    }

    [Fact]
    public async Task SearchPatient_ReturnsNotFoundWhenGettingNull() {
        _repo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO1()), Patient.createFromDTO(SeedPatientDTO2())}));
        // Act
        var result = await _controller.SearchAndFilterPatients(SeedFilterPatientDTOWithSensitiveData());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}