using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure.Shared.MessageSender;
using DDDSample1.Domain.DomainLogs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DDDSample1.ControllerTests;

public class PatientControllerTests
{
    private readonly IPatientRepository _repo;
    private readonly IDomainLogRepository _logrepo;
    private readonly IUnitOfWork _unit;
    private readonly UserManager<IdentityUser> _manager;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly Mock<PatientService> _mockService;
    private readonly PatientController _controller;

    public PatientControllerTests()
    {
        _mockMessageSender = new Mock<IMessageSenderService>();
        _mockService = new Mock<PatientService>(_repo, _unit, _logrepo, _mockMessageSender.Object);
        _controller = new PatientController(_mockService.Object, _manager);
    }

    private PatientDTO SeedPatientDTO(){
        return new PatientDTO("202410000001", DateOnly.Parse("2004-07-10"),"teste@gmail.com","987876765","Male","John One Two Doe", "Dogs, Cats");
    }
    private FilterPatientDTO SeedFilterPatientDTO(){
        return new FilterPatientDTO{Email = "novo@gmail.com"};
    }

    [Fact]
    public async Task CreatePatient_ReturnsCreatedAtAction_WithPatientDTO() {
        // Arrange
        var patientDto = SeedPatientDTO();
        
        // Setup mock to return the DTO when CreatePatient is called
        _mockService.Setup(s => s.CreatePatient(It.IsAny<PatientDTO>()))
            .ReturnsAsync(patientDto);

        // Act
        var result = await _controller.CreatePatient(patientDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);
        Assert.Equal(patientDto, returnValue);
    }

    [Fact]
    public async Task EditPatient_ReturnsNotFoundWhenGettingNull() {
        // Setup mock to return Task with null when DeletePatient is called
        _mockService.Setup(s => s.EditPatient(It.IsAny<MedicalRecordNumber>(), 
            It.IsAny<FilterPatientDTO>())).Returns(Task.FromResult<PatientDTO>(null));

        // Act
        var result = await _controller.EditPatient("202410000004", SeedFilterPatientDTO()); //Random MedicalRecordNumber

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task EditPatient_ReturnsOkAndDTOWhenGettingDTO() {
        // Arrange
        var patientDto = SeedPatientDTO();
        
        // Setup mock to return the DTO when DeletePatient is called
        _mockService.Setup(s => s.EditPatient(It.IsAny<MedicalRecordNumber>(),
            It.IsAny<FilterPatientDTO>())).ReturnsAsync(patientDto);

        // Act
        var result = await _controller.EditPatient(patientDto.MedicalRecordNumber, SeedFilterPatientDTO());

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);

        //DTO Content change is not tested here since that logic is in the Service
    }

    [Fact]
    public async Task DeletePatient_ReturnsNotFoundWhenGettingNull() {
        // Setup mock to return Task with null when DeletePatient is called
        _mockService.Setup(s => s.DeletePatient(It.IsAny<MedicalRecordNumber>()))
            .Returns(Task.FromResult<PatientDTO>(null));

        // Act
        var result = await _controller.DeletePatient("202410000004"); //Random MedicalRecordNumber

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeletePatient_ReturnsOkAndDTOWhenGettingDTO() {
        // Arrange
        var patientDto = SeedPatientDTO();
        
        // Setup mock to return the DTO when DeletePatient is called
        _mockService.Setup(s => s.DeletePatient(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync(patientDto);

        // Act
        var result = await _controller.DeletePatient(patientDto.MedicalRecordNumber);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);

        Assert.Equal(patientDto, returnValue);
    }
}