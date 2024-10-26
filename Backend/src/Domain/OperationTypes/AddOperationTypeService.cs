using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationTypes;
using System.Linq;


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

        return operationType.returnDTO();
    }

    
    public async Task<OperationTypeDTO> DeactivateOperationType(string name) {
        var operationType = await GetOperationTypeIdByName(name);
        operationType.Status = Status.INACTIVE;
        _repository.Update(operationType);
        await _unitOfWork.CommitAsync();
        
        return operationType.returnDTO();
    }

    public async Task<IEnumerable<OperationTypeDTO>> GetAll() {
        var list = _repository.GetAllAsync();
        return (await list).Select(type => type.returnDTO());
    }

    private async Task<OperationType> GetOperationTypeIdByName(string name) {
        var list = await _repository.GetAllAsync();
        return list.FirstOrDefault(type => type.name.name == name);
    }
}
