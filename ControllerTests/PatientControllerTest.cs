using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Controllers;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System;

namespace DDDSample1.ControllerTests;

public class PatientControllerTests
{
    private readonly Mock<PatientService> _mockService;
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly PatientController _controller;

    public PatientControllerTests()
    {
        _mockService = new Mock<PatientService>();
        _mockUserManager = new Mock<UserManager<IdentityUser>>();
        _controller = new PatientController(_mockService.Object, _mockUserManager.Object);
    }

    private PatientDTO SeedPatientDTO(){
        return new PatientDTO(new MedicalRecordNumber("202410000001"), DateOnly.Parse("2004-07-10"),"teste@gmail.com","987876765",Gender.Male,"John One Two Doe", "Dogs, Cats");
    }

    [Fact]
    public async Task CreatePatient_ReturnsCreatedAtAction_WithPatientDTO()
    {
        // Arrange
        var patientDto = SeedPatientDTO();

        // Set up the mock service to return the dto when CreatePatient is called
        _mockService.Setup(s => s.CreatePatient(It.IsAny<PatientDTO>()))
            .ReturnsAsync(patientDto);

        // Act
        var result = await _controller.CreatePatient(patientDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result); // Verify it returns CreatedAtActionResult
        var returnValue = Assert.IsType<PatientDTO>(actionResult.Value); // Verify the returned object is of type PatientDTO
        Assert.Equal(patientDto, returnValue); // Verify the correct PatientDTO is returned
        Assert.Equal("Patient creation", actionResult.ActionName); // Verify the action name
    }
}