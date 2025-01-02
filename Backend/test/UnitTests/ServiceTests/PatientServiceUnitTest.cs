using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.DomainLogs;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Infrastructure.Shared.MessageSender;
using FluentAssertions;
using Moq;
using Xunit;

namespace Backend.UnitTests.ServiceTests;

public class PatientServiceUnitTest {
    private readonly Mock<IUnitOfWork> _mockUnit;
    private readonly Mock<IPatientRepository> _mockPatRepo;
    private readonly Mock<IDomainLogRepository> _mockLogRepo;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly PatientService _service;
    public PatientServiceUnitTest(){
        _mockUnit = new Mock<IUnitOfWork>();
        _mockPatRepo = new Mock<IPatientRepository>();
        _mockLogRepo = new Mock<IDomainLogRepository>();
        _mockMessageSender = new Mock<IMessageSenderService>();
        _service = new PatientService(_mockUnit.Object, _mockPatRepo.Object, _mockLogRepo.Object, _mockMessageSender.Object);
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
    private PatientDTO SeedPatientDTOWithoutMedicalRecord(){
        return new PatientDTO("", DateOnly.Parse("2004-07-10"),"teste@gmail.com","987876765","Male","John One Two Doe", "Dogs, Cats");
    }
    private FilterPatientDTO SeedFilterPatientDTOWithSensitiveData(){
        return new FilterPatientDTO{Email = "novo@gmail.com", PhoneNumber = "912834756"};
    }
    private FilterPatientDTO SeedFilterPatientDTO(){
        return new FilterPatientDTO{FullName = "Joan of Arc", Allergies = "Dogs"};
    }
    private FilterPatientDTO SeedFilterPatientDTOWithOnlyGender(){
        return new FilterPatientDTO{Gender = "Male"};
    }

    [Fact]
    public async Task CreatePatient_ReturnsDTOWithThirdMedicalRecord() {
        PatientDTO dto = SeedPatientDTOWithoutMedicalRecord();
        //Setup
        _mockPatRepo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO1()), Patient.createFromDTO(SeedPatientDTO2())}));
        // Act
        var result = await _service.CreatePatient(dto);
        // Assert
        Assert.Equal(result.MedicalRecordNumber, string.Format("{0}{1:D2}000003", DateTime.Today.Year, DateTime.Today.Month));
        Assert.Equal(dto.DateOfBirth, result.DateOfBirth);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(dto.Gender, result.Gender);
        Assert.Equal(dto.FullName, result.FullName);
        Assert.Equal(dto.Allergies, result.Allergies);
    }

    [Fact]
    public async Task CreatePatient_ReturnsDTOWithFirstMedicalRecord() {
        PatientDTO dto = SeedPatientDTOWithoutMedicalRecord();
        //Setup
        _mockPatRepo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>()));
        // Act
        var result = await _service.CreatePatient(dto);
        // Assert
        Assert.Equal(result.MedicalRecordNumber, string.Format("{0}{1:D2}000001", DateTime.Today.Year, DateTime.Today.Month));
        Assert.Equal(dto.DateOfBirth, result.DateOfBirth);
        Assert.Equal(dto.Email, result.Email);
        Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(dto.Gender, result.Gender);
        Assert.Equal(dto.FullName, result.FullName);
        Assert.Equal(dto.Allergies, result.Allergies);
    }

    [Fact]
    public async Task EditPatient_ReturnsNullWhenPatientDoesntExist() {
        // Setup mock to return Task with null when EditPatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync((Patient)null);

        // Act
        var result = await _service.EditPatient(new MedicalRecordNumber("202410000004"), SeedFilterPatientDTO()); //Random MedicalRecordNumber

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task EditPatient_ReturnsDTOWhenPatientExists() {
        var patientDto = SeedPatientDTO1();
        // Setup mock to return Task with DTO when EditPatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync(Patient.createFromDTO(patientDto));

        // Act
        var result = await _service.EditPatient(
                new MedicalRecordNumber(patientDto.MedicalRecordNumber), //Random MedicalRecordNumber
                SeedFilterPatientDTO()); 

        // Assert
        Assert.Equal(patientDto.Email, result.Email);
        Assert.Equal(patientDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(patientDto.DateOfBirth, result.DateOfBirth);
        Assert.NotEqual(patientDto.FullName, result.FullName);
        Assert.NotEqual(patientDto.Allergies, result.Allergies);
    }

    [Fact]
    public async Task EditPatient_ReturnsDTOWhenPatientExistsWithSensitiveData() {
        var patientDto = SeedPatientDTO1();
        // Setup mock to return Task with DTO when EditPatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync(Patient.createFromDTO(patientDto));

        // Act
        var result = await _service.EditPatient(
                new MedicalRecordNumber(patientDto.MedicalRecordNumber), //Random MedicalRecordNumber
                SeedFilterPatientDTOWithSensitiveData()); 

        // Assert
        Assert.NotEqual(patientDto.Email, result.Email);
        Assert.NotEqual(patientDto.PhoneNumber, result.PhoneNumber);
        Assert.Equal(patientDto.DateOfBirth, result.DateOfBirth);
        Assert.Equal(patientDto.FullName, result.FullName);
        Assert.Equal(patientDto.Allergies, result.Allergies);
        //The patient would receive a notification in their old email address, but the MessageSenderService is Mockd for this test
    }    

    [Fact]
    public async Task DeletePatient_ReturnsNullWhenPatientDoesntExist() {
        // Setup mock to return Task with null when DeletePatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync((Patient)null);

        // Act
        var result = await _service.DeletePatient(new MedicalRecordNumber("202410000004")); //Random MedicalRecordNumber

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public async Task DeletePatient_ReturnsDTOWhenPatientExists() {
        var patientDto = SeedPatientDTO1();
        // Setup mock to return Task with DTO when DeletePatient is called
        _mockPatRepo.Setup(r => r.GetPatientByRecordNumber(It.IsAny<MedicalRecordNumber>()))
            .ReturnsAsync(Patient.createFromDTO(patientDto));

        // Act
        var result = await _service.DeletePatient(new MedicalRecordNumber(patientDto.MedicalRecordNumber)); //Random MedicalRecordNumber

        // Assert
        result.Should().BeEquivalentTo(patientDto);
    }

    [Fact]
    public async Task SearchPatient_ReturnsNullWhenListEmpty() {
        PatientDTO dto = SeedPatientDTOWithoutMedicalRecord();
        //Setup
        _mockPatRepo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO1()), Patient.createFromDTO(SeedPatientDTO2())}));
        // Act
        var result = await _service.SearchPatients(SeedFilterPatientDTOWithSensitiveData()); //Email and PhoneNumber won't appear
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task SearchPatient_ReturnsFilteredListWhenListNotEmptyEmailPhoneNumber() {
        PatientDTO dto = SeedPatientDTOWithoutMedicalRecord();
        //Setup
        _mockPatRepo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO1()), Patient.createFromDTO(SeedPatientDTO2()), Patient.createFromDTO(SeedPatientDTO3())}));
        // Act
        var results = await _service.SearchPatients(SeedFilterPatientDTOWithSensitiveData()); //Email and PhoneNumber will match with third patient
        var result = Assert.Single(results);
        // Assert
        Assert.Equal("novo@gmail.com", result.Email);
        Assert.Equal("912834756", result.PhoneNumber);
    }

    [Fact]
    public async Task SearchPatient_ReturnsFilteredListWhenListNotEmptyGender() {
        PatientDTO dto = SeedPatientDTOWithoutMedicalRecord();
        //Setup
        _mockPatRepo.Setup(r => r.GetAllAsync())
            .Returns(Task.FromResult(new List<Patient>{Patient.createFromDTO(SeedPatientDTO1()), Patient.createFromDTO(SeedPatientDTO2()), Patient.createFromDTO(SeedPatientDTO3())}));
        // Act
        var results = await _service.SearchPatients(SeedFilterPatientDTOWithOnlyGender()); //Two results will appear
        // Assert
        Assert.All(results, result => Assert.Equal("Male", result.Gender));
    }
}