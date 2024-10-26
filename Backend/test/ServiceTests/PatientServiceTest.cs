using System;
using System.Threading.Tasks;
using DDDSample1.Domain.DomainLogs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure.Shared.MessageSender;
using FluentAssertions;
using Moq;
using Xunit;

namespace DDDSample1.ServiceTests;

public class PatientServiceTest {
    private readonly Mock<IUnitOfWork> _mockUnit;
    private readonly Mock<IPatientRepository> _mockPatRepo;
    private readonly Mock<IDomainLogRepository> _mockLogRepo;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly PatientService _service;
    public PatientServiceTest(){
        _mockUnit = new Mock<IUnitOfWork>();
        _mockPatRepo = new Mock<IPatientRepository>();
        _mockLogRepo = new Mock<IDomainLogRepository>();
        _mockMessageSender = new Mock<IMessageSenderService>();
        _service = new PatientService(_mockUnit.Object, _mockPatRepo.Object, _mockLogRepo.Object, _mockMessageSender.Object);
    }
    private PatientDTO SeedPatientDTO(){
        return new PatientDTO("202410000001", DateOnly.Parse("2004-07-10"),"teste@gmail.com","987876765","Male","John One Two Doe", "Dogs, Cats");
    }
    [Fact]
    public async Task DeletePatient_ReturnsNullWhenPatientDoesntExist() {
        // Setup mock to return Task with null when DeletePatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .Returns((Patient)null);

        // Act
        var result = await _service.DeletePatient(new MedicalRecordNumber("202410000004")); //Random MedicalRecordNumber

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task DeletePatient_ReturnsDTOWhenPatientExists() {
        var patientDto = SeedPatientDTO();
        // Setup mock to return Task with null when DeletePatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .Returns(Patient.createFromDTO(patientDto));

        // Act
        var result = await _service.DeletePatient(new MedicalRecordNumber(patientDto.MedicalRecordNumber)); //Random MedicalRecordNumber

        // Assert
        result.Should().BeEquivalentTo(patientDto);
    }
}