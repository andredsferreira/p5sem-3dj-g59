using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Controllers;
using Backend.Domain.OperationRequests;
using Backend.Domain.OperationTypes;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Domain.Shared.Exceptions;
using Backend.Domain.Staffs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Backend.ControllerTests {
    public class OperationRequestControllerUnitTests {

        private readonly Mock<OperationRequestService> _mockService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _operationRequestRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly OperationRequestController _controller;

        public OperationRequestControllerUnitTests() {
            _mockService = new Mock<OperationRequestService>(_httpContextAccessor, _unitOfWork, _operationRequestRepository, _patientRepository, _staffRepository, _operationTypeRepository);
            _controller = new OperationRequestController(_mockService.Object);
        }

        [Fact]
        public async Task TestCreation_Success() {
            //Arrange
            var dto = new CreateOperationRequestDTO{
                medicalRecordNumber= "202410000001",
                operationTypeId= Guid.NewGuid(),
                priority= "High",
                dateTime= DateTime.Now,
                requestStatus= "Pending"
            };

            _mockService.Setup(service => service.CreateOperationRequest(dto)).ReturnsAsync(Guid.NewGuid());

            //Act
            var result = await _controller.CreateOperationRequest(dto);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestCreation_PatientNotFoundException() {
            //Arrange
            var dto = new CreateOperationRequestDTO{
                medicalRecordNumber= "202410000001",
                operationTypeId= Guid.NewGuid(),
                priority= "High",
                dateTime= DateTime.Now,
                requestStatus= "Pending"
            };

            _mockService.Setup(service => service.CreateOperationRequest(dto))
                .ThrowsAsync(new PatientNotFoundException("Patient not found"));

            //Act
            var result = await _controller.CreateOperationRequest(dto);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ error = Patient not found }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task TestUpdate_Success() {
            //Arrange
            var updatedDto = new UpdateOperationRequestDTO(
                updatedId: Guid.NewGuid(),
                priority: "Medium",
                dateTime: DateTime.Now
            );

            _mockService.Setup(service => service.UpdateOperationRequest(updatedDto)).Returns(Task.FromResult(updatedDto.updatedId));

            //Act
            var result = await _controller.UpdateOperationRequest(updatedDto);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains(updatedDto.updatedId.ToString(), okResult.Value.ToString());
        }

        [Fact]
        public async Task TestUpdate_OperationRequestNotFoundException() {
            //Arrange
            var updatedDto = new UpdateOperationRequestDTO(
                updatedId: Guid.NewGuid(),
                priority: "Medium",
                dateTime: DateTime.Now
            );

            _mockService.Setup(service => service.UpdateOperationRequest(updatedDto))
                .ThrowsAsync(new OperationRequestNotFoundException("Operation request not found"));

            //Act
            var result = await _controller.UpdateOperationRequest(updatedDto);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ error = Operation request not found }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task TestDelete_Success() {
            //Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteOperationRequest(id)).ReturnsAsync(id);

            //Act
            var result = await _controller.DeleteOperationRequest(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestDelete_OperationRequestNotFoundException() {
            //Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteOperationRequest(id))
                .ThrowsAsync(new OperationRequestNotFoundException("Operation request not found"));

            //Act
            var result = await _controller.DeleteOperationRequest(id);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("{ error = Operation request not found }", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task TestList_Success() {
            //Arrange
            var operationRequests = new List<ListOperationRequestDTO> {
                new ListOperationRequestDTO{
                    operationRequestId = Guid.NewGuid(),
                    doctorName = "Tiago",
                    medicalRecordNumber = "202410000001",
                    patientFullName = "Tiago",
                    operationTypeName = "ACL Reconstruction",
                    priority = "High",
                    dateTime = DateTime.Now,
                    requestStatus = "Pending"
                }
            };

            _mockService.Setup(service => service.ListOperationRequests()).ReturnsAsync(operationRequests);

            //Act
            var result = await _controller.ListOperationRequests();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRequests = Assert.IsAssignableFrom<List<ListOperationRequestDTO>>(okResult.Value);
            Assert.Single(returnedRequests);
        }

        [Fact]
        public async Task TestList_NotFound() {
            //Arrange
            var operationRequests = new List<ListOperationRequestDTO>();

            _mockService.Setup(service => service.ListOperationRequests()).ReturnsAsync(operationRequests);

            //Act
            var result = await _controller.ListOperationRequests();
//
            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No operation requests were found", notFoundResult.Value);
        }
    }
}
