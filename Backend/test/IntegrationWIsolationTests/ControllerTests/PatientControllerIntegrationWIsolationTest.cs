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

public class PatientControllerIntegrationWIsolationTests
{
    private readonly IPatientRepository _repo;
    private readonly IDomainLogRepository _logrepo;
    private readonly IUnitOfWork _unit;
    private readonly UserManager<IdentityUser> _manager;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly PatientService service;
    private readonly PatientController _controller;

    public PatientControllerIntegrationWIsolationTests()
    {
        _mockMessageSender = new Mock<IMessageSenderService>();
        service = new PatientService(_unit, _repo, _logrepo, _mockMessageSender.Object);
        _controller = new PatientController(service, _manager);
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

        // Act
        var result = await _controller.CreatePatient(patientDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);
        Assert.Equal(patientDto, returnValue);
    }

    [Fact]
    public async Task EditPatient_ReturnsNotFoundWhenGettingNull() {

        // Act
        var result = await _controller.EditPatient("202410000004", SeedFilterPatientDTO()); //Random MedicalRecordNumber

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task EditPatient_ReturnsOkAndDTOWhenGettingDTO() {
        // Arrange
        var patientDto = SeedPatientDTO();

        // Act
        var result = await _controller.EditPatient(patientDto.MedicalRecordNumber, SeedFilterPatientDTO());

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<PatientDTO>(actionResult.Value);

        //DTO Content change is not tested here since that logic is in the Service
    }

    [Fact]
    public async Task DeletePatient_ReturnsNotFoundWhenGettingNull() {

        // Act
        var result = await _controller.DeletePatient("202410000004"); //Random MedicalRecordNumber

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeletePatient_ReturnsOkAndDTOWhenGettingDTO() {
        // Arrange
        var patientDto = SeedPatientDTO();

        // Act
        var result = await _controller.DeletePatient(patientDto.MedicalRecordNumber);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value);

        Assert.Equal(patientDto, returnValue);
    }

    [Fact]
    public async Task SearchPatient_ReturnsOkAndListDTOWhenGettingList() {

        // Act
        var result = await _controller.SearchAndFilterPatients(SeedFilterPatientDTO());

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<PatientDTO>>(actionResult.Value);
    }

    [Fact]
    public async Task SearchPatient_ReturnsNotFoundWhenGettingNull() {

        // Act
        var result = await _controller.SearchAndFilterPatients(SeedFilterPatientDTO());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}