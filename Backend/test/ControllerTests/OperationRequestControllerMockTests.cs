// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using DDDSample1.Controllers;
// using DDDSample1.Domain.OperationRequests;
// using DDDSample1.Domain.Shared.Exceptions;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Xunit;

// namespace DDDSample1.ControllerTests {
//     public class OperationRequestControllerMockTests {

//         private readonly Mock<OperationRequestService> _mockService;
//         private readonly OperationRequestController _controller;

//         public OperationRequestControllerMockTests() {
//             _mockService = new Mock<OperationRequestService>();
//             _controller = new OperationRequestController(_mockService.Object);
//         }

//         [Fact]
//         public async Task TestCreation_Success() {
//             // Arrange
//             var dto = new OperationRequestDTO(
//                 patientId: Guid.NewGuid(),
//                 operationTypeId: Guid.NewGuid(),
//                 priority: "High",
//                 dateTime: DateTime.Now,
//                 requestStatus: "Pending"
//             );

//             _mockService.Setup(service => service.CreateOperationRequest(dto)).ReturnsAsync(dto);

//             // Act
//             var result = await _controller.CreateOperationRequest(dto);

//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             Assert.Equal(dto, okResult.Value);
//         }

//         [Fact]
//         public async Task TestCreation_PatientNotFoundException() {
//             // Arrange
//             var dto = new OperationRequestDTO(
//                 patientId: Guid.NewGuid(),
//                 operationTypeId: Guid.NewGuid(),
//                 priority: "High",
//                 dateTime: DateTime.Now,
//                 requestStatus: "Pending"
//             );

//             _mockService.Setup(service => service.CreateOperationRequest(dto))
//                 .ThrowsAsync(new PatientNotFoundException("Patient not found"));

//             // Act
//             var result = await _controller.CreateOperationRequest(dto);

//             // Assert
//             var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//             Assert.Equal("Patient not found", badRequestResult.Value.ToString());
//         }

//         [Fact]
//         public async Task TestUpdate_Success() {
//             // Arrange
//             var updatedDto = new UpdatedOperationRequestDTO(
//                 updatedId: Guid.NewGuid(),
//                 priority: "Medium",
//                 dateTime: DateTime.Now
//             );

//             _mockService.Setup(service => service.UpdateOperationRequest(updatedDto)).ReturnsAsync(updatedDto);

//             // Act
//             var result = await _controller.UpdateOperationRequest(updatedDto);

//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             Assert.Equal(updatedDto, okResult.Value);
//         }

//         [Fact]
//         public async Task TestUpdate_OperationRequestNotFoundException() {
//             // Arrange
//             var updatedDto = new UpdatedOperationRequestDTO(
//                 updatedId: Guid.NewGuid(),
//                 priority: "Medium",
//                 dateTime: DateTime.Now
//             );

//             _mockService.Setup(service => service.UpdateOperationRequest(updatedDto))
//                 .ThrowsAsync(new OperationRequestNotFoundException("Operation request not found"));

//             // Act
//             var result = await _controller.UpdateOperationRequest(updatedDto);

//             // Assert
//             var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//             Assert.Equal("Operation request not found", badRequestResult.Value.ToString());
//         }

//         [Fact]
//         public async Task TestDelete_Success() {
//             // Arrange
//             var id = Guid.NewGuid();
//             _mockService.Setup(service => service.DeleteOperationRequest(id)).ReturnsAsync(id);

//             // Act
//             var result = await _controller.DeleteOperationRequest(id);

//             // Assert
//             Assert.IsType<OkResult>(result);
//         }

//         [Fact]
//         public async Task TestDelete_OperationRequestNotFoundException() {
//             // Arrange
//             var id = Guid.NewGuid();
//             _mockService.Setup(service => service.DeleteOperationRequest(id))
//                 .ThrowsAsync(new OperationRequestNotFoundException("Operation request not found"));

//             // Act
//             var result = await _controller.DeleteOperationRequest(id);

//             // Assert
//             var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//             Assert.Equal("Operation request not found", badRequestResult.Value.ToString());
//         }

//         [Fact]
//         public async Task TestList_Success() {
//             // Arrange
//             var operationRequests = new List<OperationRequestDTO> {
//                 new OperationRequestDTO(
//                     operationRequestId: Guid.NewGuid(),
//                     staffId: Guid.NewGuid(),
//                     patientId: Guid.NewGuid(),
//                     operationTypeId: Guid.NewGuid(),
//                     priority: "High",
//                     dateTime: DateTime.Now,
//                     requestStatus: "Pending"
//                 )
//             };

//             _mockService.Setup(service => service.ListOperationRequests()).ReturnsAsync(operationRequests);

//             // Act
//             var result = await _controller.ListOperationRequests();

//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             var returnedRequests = Assert.IsAssignableFrom<List<OperationRequestDTO>>(okResult.Value);
//             Assert.Single(returnedRequests);
//         }

//         [Fact]
//         public async Task TestList_NotFound() {
//             // Arrange
//             var operationRequests = new List<OperationRequestDTO>();

//             _mockService.Setup(service => service.ListOperationRequests()).ReturnsAsync(operationRequests);

//             // Act
//             var result = await _controller.ListOperationRequests();

//             // Assert
//             var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
//             Assert.Equal("No operation requests were found", notFoundResult.Value);
//         }
//     }
// }
