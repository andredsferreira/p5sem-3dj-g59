using System.Net.Mail;
using System.Threading.Tasks;
using Backend.Controllers;
using Backend.Domain.DomainLogs;
using Backend.Domain.Shared;
using Backend.Domain.Staffs;
using Backend.Infrastructure.Shared.MessageSender;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class StaffControllerIntegrationWIsolationTests
{
    private readonly Mock<IStaffRepository> _repo;
    private readonly Mock<IUnitOfWork> _unit;
    private readonly StaffService service;
    private readonly StaffController _controller;
    private readonly Mock<IMessageSenderService> _mockMessageSender;
    private readonly Mock<IDomainLogRepository> _logrepo;

    public StaffControllerIntegrationWIsolationTests()
    {
        // Criação dos mocks
        _unit = new Mock<IUnitOfWork>();
        _repo = new Mock<IStaffRepository>();
        _mockMessageSender = new Mock<IMessageSenderService>();
        _logrepo = new Mock<IDomainLogRepository>();

        // Instanciação do serviço e controlador
        service = new StaffService(_unit.Object, _repo.Object, _logrepo.Object, _mockMessageSender.Object);
        _controller = new StaffController(service);
    }

    private StaffDTO SeedStaffDTO()
    {
        return new StaffDTO("Doctor", "johndoe", "johndoe@example.com", "987654321", "John Doe", "LN12345");
    }

    private FilterStaffDTO SeedFilterStaffDTO()
    {
        return new FilterStaffDTO { Email = "johndoe@example.com" };
    }

    private Staff SeedStaff()
    {
        return new Staff("Doctor", "johndoe", new MailAddress("johndoe@example.com"), new PhoneNumber("987654321"), new FullName("John Doe"), new LicenseNumber("LN12345"));
    }

    [Fact]
    public async Task CreateStaff_ReturnsCreatedAtAction_WithStaffDTO()
    {
        // Arrange
        var staffDto = SeedStaffDTO();
        var existingStaff = SeedStaff();

        _repo.Setup(repo => repo.GetByIdentityUsername(It.IsAny<string>())).Returns((Staff)null);
        _repo.Setup(repo => repo.AddAsync(It.IsAny<Staff>())).ReturnsAsync(existingStaff);
        _unit.Setup(u => u.CommitAsync()).ReturnsAsync(1);

        // Act
        var result = await _controller.CreateStaff(staffDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<StaffDTO>(actionResult.Value);

        Assert.Equal(staffDto.Email, returnValue.Email);
    }

    [Fact]
    public async Task EditStaff_ReturnsNotFoundWhenStaffDoesNotExist()
    {
        // Arrange
        var staffDto = SeedFilterStaffDTO();
        _repo.Setup(r => r.GetByIdentityUsername(It.IsAny<string>())).Returns((Staff)null);

        // Act
        var result = await _controller.EditStaff("johndoe", staffDto);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

   

    [Fact]
    public async Task DeleteStaff_ReturnsNotFoundWhenStaffDoesNotExist()
    {
        // Arrange
        _repo.Setup(r => r.GetByIdentityUsername(It.IsAny<string>())).Returns((Staff)null);

        // Act
        var result = await _controller.DeleteStaff("johndoe");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}
