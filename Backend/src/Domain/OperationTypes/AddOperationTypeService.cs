using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.OperationTypes;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace DDDSample1.Domain.OperationTypes;
public class AddOperationTypeService {

    private readonly IOperationTypeRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    public AddOperationTypeService(IOperationTypeRepository repository, IUnitOfWork unitOfWork) {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationTypeDTO> CreateOperationType([FromForm] OperationTypeDTO dto) {
        
        OperationType operationType = OperationType.createFromDTO(dto);
        
        await _repository.AddAsync(operationType);
        await _unitOfWork.CommitAsync();

        return operationType.returnDTO();
    }

    
    public async Task<OperationTypeDTO> DeactivateOperationType(string id) {
        var operationType = await _repository.GetByIdAsync(new OperationTypeId(id));
        if (operationType == null) {
            throw new Exception("Operation Type not found.");
        }
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

    public async Task<OperationTypeDTO> GetOperationTypeById(string id) {
        var operationType = await _repository.GetByIdAsync(new OperationTypeId(id));
        return operationType.returnDTO();
    }

    public async Task<OperationTypeDTO> GetOperationTypeByName(string name) {
        var operationType = await GetOperationTypeIdByName(name);
        if (operationType == null) {
            throw new Exception("Operation Type not found.");
        }
        return operationType.returnDTO();
    }

    public async Task<OperationTypeDTO> UpdateOperationType([FromForm] UpdatedOperationTypeDTO dto, string id) {
        var operationType = this._repository.GetByIdAsync(new OperationTypeId(id)).Result;
        
        if (operationType == null) {
            throw new Exception("Operation Type not found.");
        }

        operationType.name = new OperationName(dto.name);
        operationType.anaesthesiaTime = new AnaesthesiaTime(dto.anaesthesiaTime);
        operationType.surgeryTime = new SurgeryTime(dto.surgeryTime);
        operationType.cleaningTime = new CleaningTime(dto.cleaningTime);
        

        await _unitOfWork.CommitAsync();
        return operationType.returnDTO();
    }

    public async Task<IEnumerable<OperationTypeDTO>> GetBySpecialization(string specialization) {
        var list = await _repository.GetAllAsync();
        return list.Where(type => type.Specialization.ToString() == specialization).Select(type => type.returnDTO());
    }

}
