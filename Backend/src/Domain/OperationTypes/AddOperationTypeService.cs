using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationTypes;


namespace DDDSample1.Domain.OperationTypes;
public class AddOperationTypeService {

    private readonly IOperationTypeRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    public AddOperationTypeService(IOperationTypeRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationTypeDTO> CreateOperationType(string name, int anaesthesiaTime, int surgeryTime, int cleaningTime) {
        OperationType operationType = new OperationType(new OperationName(name),
        new AnaesthesiaTime(anaesthesiaTime), new SurgeryTime(surgeryTime), new CleaningTime(cleaningTime));

        operationType.Status = Status.ACTIVE;

        await _repository.AddAsync(operationType);

        return new OperationTypeDTO() {
            id = operationType.Id.AsGuid(),
            name = operationType.name.ToString(),
            anaesthesiaTime = operationType.anaesthesiaTime.duration,
            surgeryTime = operationType.surgeryTime.duration,
            cleaningTime = operationType.cleaningTime.duration
        };
    }

    
    public async Task<OperationTypeDTO> DeactivateOperationType(Guid id) {
        var operationType = await _repository.GetByIdAsync(new OperationTypeId(id));
        operationType.Status = Status.INACTIVE;
        _repository.Update(operationType);
        await _unitOfWork.CommitAsync();
        
        return new OperationTypeDTO() {
            id = operationType.Id.AsGuid(),
            name = operationType.name.ToString(),
            anaesthesiaTime = operationType.anaesthesiaTime.duration,
            surgeryTime = operationType.surgeryTime.duration,
            cleaningTime = operationType.cleaningTime.duration
        };
    }

    public async Task<List<OperationType>> GetAll() {
        var list = _repository.GetAllAsync();
        return await list;
    }
}
