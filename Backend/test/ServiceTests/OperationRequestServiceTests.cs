using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Http;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.OperationTypes;

public class OperationRequestServiceTests {

    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly Mock<IOperationRequestRepository> _mockOperationRequestRepository;

    private readonly Mock<IPatientRepository> _mockPatientRepository;

    private readonly Mock<IStaffRepository> _mockStaffRepository;

    private readonly Mock<IOperationTypeRepository> _mockOperationTypeRepository;

    private readonly OperationRequestService _service;

    public OperationRequestServiceTests() {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockOperationRequestRepository = new Mock<IOperationRequestRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockPatientRepository = new Mock<IPatientRepository>();
        _mockStaffRepository = new Mock<IStaffRepository>();
        _mockOperationTypeRepository = new Mock<IOperationTypeRepository>();

        _service = new OperationRequestService(
            _mockHttpContextAccessor.Object,
            _mockUnitOfWork.Object,
            _mockOperationRequestRepository.Object,
            _mockPatientRepository.Object,
            _mockStaffRepository.Object,
            _mockOperationTypeRepository.Object);
    }

    [Fact]
    public async Task testCreation() {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task testUpdate() {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task testDelete() {
        throw new NotImplementedException();
    }

    [Fact]
    public async Task testList() {
        throw new NotImplementedException();
    }

}
