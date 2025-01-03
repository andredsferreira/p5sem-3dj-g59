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
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Linq;

namespace Backend.UnitTests.ControllerTests;

public class PatientControllerUnitTests {
    private readonly IPatientRepository _repo;
    private readonly IDomainLogRepository _logrepo;
    private readonly IUnitOfWork _unit;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly Mock<PatientService> _mockService;
    private readonly PatientController _controller;

    public PatientControllerUnitTests() {
        _mockMessageSender = new Mock<IMessageSenderService>();
        _mockService = new Mock<PatientService>(_repo, _unit, _logrepo, _mockMessageSender.Object);
        _controller = new PatientController(_mockService.Object);
    }

    private PatientDTO SeedPatientDTO() {
        return new PatientDTO("202410000001", DateOnly.Parse("2004-07-10"), "teste@gmail.com", "987876765", "Male", "John One Two Doe", "Dogs, Cats");
    }
    private FilterPatientDTO SeedFilterPatientDTO() {
        return new FilterPatientDTO { Email = "novo@gmail.com" };
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
        // Setup mock to return Task with null when EditPatient is called
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

        // Setup mock to return the DTO when EditPatient is called
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

    [Fact]
    public async Task SearchPatient_ReturnsOkAndListDTOWhenGettingList() {
        // Setup mock to return the list of DTOs when SearchAndFilterPatients is called
        _mockService.Setup(s => s.SearchPatients(It.IsAny<FilterPatientDTO>()))
            .Returns(Task.FromResult(new List<PatientDTO> { SeedPatientDTO() }.AsEnumerable()));

        // Act
        var result = await _controller.SearchAndFilterPatients(SeedFilterPatientDTO());

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<PatientDTO>>(actionResult.Value);
    }

    [Fact]
    public async Task SearchPatient_ReturnsNotFoundWhenGettingNull() {
        // Setup mock to return null when SearchAndFilterPatients is called
        _mockService.Setup(s => s.SearchPatients(It.IsAny<FilterPatientDTO>()))
            .Returns(Task.FromResult((IEnumerable<PatientDTO>)null));

        // Act
        var result = await _controller.SearchAndFilterPatients(SeedFilterPatientDTO());

        // Assert
        var actionResult = Assert.IsType<NotFoundResult>(result.Result);
    }
}