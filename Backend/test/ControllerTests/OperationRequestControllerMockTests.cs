using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Controllers;
using DDDSample1.Domain.OperationRequests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DDDSample1.ControllerTests;
public class OperationRequestControllerMockTests {
    
    private readonly Mock<OperationRequestService> _mockService;

    private readonly OperationRequestController _controller;

    public OperationRequestControllerMockTests() {
        _mockService = new Mock<OperationRequestService>();
        _controller = new OperationRequestController(_mockService.Object);
    }

    [Fact]
    public async Task TestCreation() {
        var dto = new OperationRequestDTO(
            patientId: Guid.NewGuid(),
            operationTypeId: Guid.NewGuid(),
            priority: "High",
            dateTime: DateTime.Now,
            requestStatus: RequestStatus.Pending 
        );

        _mockService.Setup(service => service.CreateOperationRequest(dto)).ReturnsAsync(dto);

        var result = await _controller.CreateOperationRequest(dto);

        var actionResult = Assert.IsType<ActionResult<OperationRequestDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(dto, okResult.Value);
    }

    [Fact]
    public async Task TestUpdate() {
        var updatedDto = new UpdatedOperationRequestDTO(
            updatedId: Guid.NewGuid(),
            priority: "Medium",
            dateTime: DateTime.Now
        );

        _mockService.Setup(service => service.UpdateOperationRequest(updatedDto)).ReturnsAsync(updatedDto);

        var result = await _controller.UpdateOperationRequest(updatedDto);

        var actionResult = Assert.IsType<ActionResult<UpdatedOperationRequestDTO>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(updatedDto, okResult.Value);
    }

    [Fact]
    public async Task TestDelete() {
        var id = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteOperationRequest(id)).ReturnsAsync(id);

        var result = await _controller.DeleteOperationRequest(id);

        var actionResult = Assert.IsType<ActionResult<Guid>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.Equal(id, okResult.Value);
    }

    [Fact]
    public async Task TestList() {
        // Arrange
        var operationRequests = new List<OperationRequestDTO> {
                new OperationRequestDTO(patientId: Guid.NewGuid(), operationTypeId: Guid.NewGuid(), priority: "High", dateTime: DateTime.Now, requestStatus: RequestStatus.Pending)
            };

        _mockService.Setup(service => service.ListOperationRequests()).ReturnsAsync(operationRequests);

        var result = await _controller.ListOperationRequests();

        var actionResult = Assert.IsType<ActionResult<List<OperationRequestDTO>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedRequests = Assert.IsAssignableFrom<List<OperationRequestDTO>>(okResult.Value);
        Assert.Single(returnedRequests);
    }
}