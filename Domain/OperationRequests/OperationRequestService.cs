using System;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationTypes;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using Microsoft.AspNetCore.Routing;

namespace DDDSample1.Domain.OperationRequests {

    public class OperationRequestService {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IOperationRequestRepository _repository;

        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository repository) {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        // async keyword gives warning
        public  Task<OperationRequestDTO> CreateOperationRequest(OperationRequestDTO dto) {
            var patient = Patient.createFromDTO(dto.patientDTO);
            var staff = Staff.createFromDTO(dto.staffDTO);
            var operationType = OperationType.createFromDTO(dto.operationTypeDTO);
            var operationRequest = new OperationRequest(patient, staff, operationType, dto.priority, dto.dateTime, dto.requestStatus);
            throw new NotImplementedException();
        }

    }

}