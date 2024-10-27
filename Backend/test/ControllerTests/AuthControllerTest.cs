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
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Infrastructure;
using Microsoft.Extensions.Configuration;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain;

namespace DDDSample1.ControllerTests;

public class AuthControllerTests
{
    private readonly IConfiguration _configuration;

    private readonly IdentityContext _context;

    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly IMessageSenderService _messageSender;
    //-----------------------------------------------------
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly Mock<IdentityContext> _mockContext;
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly Mock<SignInManager<IdentityUser>> _mockSignInManager;
    private readonly Mock<IMessageSenderService> _mockMessageSender;

    private readonly AuthController _controller;

    public AuthControllerTests()
    {  
        _mockConfig = new Mock<IConfiguration>();
        _mockContext = new Mock<IdentityContext>(_mockConfig.Object);
        _mockUserManager = new Mock<UserManager<IdentityUser>>();
        _mockSignInManager = new Mock<SignInManager<IdentityUser>>();
        _mockMessageSender = new Mock<IMessageSenderService>();
        _controller = new AuthController(_mockConfig.Object, _mockContext.Object,
                        _mockUserManager.Object, _mockSignInManager.Object, _mockMessageSender.Object);
    }

    public RegisterBackofficeDTO SeedRegisterBackofficeDTODoctor(){
        return new RegisterBackofficeDTO{Role = "Doctor", Username = "Doctor1", Email = "doctor1@gmail.com", Phone = "999777555", Password = "StrongPassword*123"};
    }
    
    public RegisterBackofficeDTO SeedRegisterBackofficeDTOPatient(){ //Will fail
        return new RegisterBackofficeDTO{Role = "Patient", Username = "Patient1", Email = "patient1@gmail.com", Phone = "999555777", Password = "StrongPassword*456"};
    }

    //[Fact]
    //public async Task RegisterBackofficeDoctorReturnsOkAndToken() {
        // Arrange
    //    var doctorDTO = SeedRegisterBackofficeDTODoctor();
    
        // Act
    //    var result = await _controller.RegisterBackoffice(doctorDTO);

        // Assert
    //    var actionResult = Assert.IsType<OkObjectResult>(result);
    //    Assert.IsType<string>(actionResult.Value); //token
    //} Can't figure out how to mock UserManager and SignInManager correctly

}