using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Controllers;
using DDDSample1.Domain.OperationTypes;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace DDDSample1.ControllerTests;

public class OperationTypeControllerMockTests{

    private readonly Mock<AddOperationTypeService> _mockService;

    private readonly OperationTypeController _controller;

    public OperationTypeControllerMockTests() {
        _mockService = new Mock<AddOperationTypeService>();
        _controller = new OperationTypeController(_mockService.Object);
    }

    [Fact]
    public async Task TestCreation() {
        var dto = new OperationTypeDTO("nome", 1 , 1, 1, "ACTIVE", "Prosthetics",1,1,1,1,1,1,1);

        _mockService.Setup(service => service.CreateOperationType(dto)).ReturnsAsync(dto);

        var result = await _controller.AddOperationType(dto);

        var actionResult = Assert.IsType<ActionResult<OperationTypeDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(dto, okResult.Value);
    }

    [Fact]
    public async Task TestUpdate() {

         var updatedDto = new UpdatedOperationTypeDTO {
            name = "updatedName",
            anaesthesiaTime = 2,
            surgeryTime = 2,
            cleaningTime = 2
        };
        var id = "some-id";

        _mockService.Setup(service => service.UpdateOperationType(updatedDto, id)).ReturnsAsync(new OperationTypeDTO {
            name = updatedDto.name,
            anaesthesiaTime = updatedDto.anaesthesiaTime,
            surgeryTime = updatedDto.surgeryTime,
            cleaningTime = updatedDto.cleaningTime
        });

        var result = await _controller.UpdateOperationType(updatedDto, id);

        var actionResult = Assert.IsType<ActionResult<OperationTypeDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(updatedDto.name, ((OperationTypeDTO)okResult.Value).name);
   
    }

    [Fact]
    public async Task TestDelete() {

        var id = Guid.NewGuid().ToString();

        _mockService.Setup(service => service.DeactivateOperationType(id)).ReturnsAsync(new OperationTypeDTO {
            id = new Guid (id),
            Status = "INACTIVE"
        });

        var result = await _controller.DeactivateOperationType(id);

        var actionResult = Assert.IsType<ActionResult<OperationTypeDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal("INACTIVE", ((OperationTypeDTO)okResult.Value).Status);
   
    }



}