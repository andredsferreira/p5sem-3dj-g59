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

namespace DDDSample1.ControllerTests;

public class PatientControllerTests
{
    private readonly IPatientRepository _repo;
    private readonly IUnitOfWork _unit;
    private readonly UserManager<IdentityUser> _manager;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly Mock<PatientService> _mockService;
    private readonly PatientController _controller;

    public PatientControllerTests()
    {
        _mockMessageSender = new Mock<IMessageSenderService>();
        _mockService = new Mock<PatientService>(_repo, _unit, _mockMessageSender.Object);
        _controller = new PatientController(_mockService.Object, _manager);
    }

    private PatientDTO SeedPatientDTO(){
        return new PatientDTO(new MedicalRecordNumber("202410000001"), DateOnly.Parse("2004-07-10"),"teste@gmail.com","987876765",Gender.Male,"John One Two Doe", "Dogs, Cats");
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
        Assert.Equal("Patient creation", actionResult.ActionName);
    }
}