using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.Shared;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Backend.Domain.DomainLogs;


namespace Backend.Domain.OperationTypes;
public class AddOperationTypeService {

    private readonly IOperationTypeRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IDomainLogRepository _logRepository;


    public AddOperationTypeService(IOperationTypeRepository repository, IUnitOfWork unitOfWork, IDomainLogRepository logRepository) {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logRepository = logRepository;
    }

    public AddOperationTypeService() {
    }

    public virtual async Task<OperationTypeDTO> CreateOperationType([FromForm] OperationTypeDTO dto) {
        
        OperationType operationType = OperationType.createFromDTO(dto);
        
        await _repository.AddAsync(operationType);
        await _unitOfWork.CommitAsync();

        await _logRepository.AddAsync(new DomainLog(LogObjectType.OperationType, LogActionType.Creation, 
            string.Format("Created a new Operation Type (Name = {0})", operationType.name.name)));

        return operationType.returnDTO();
    }

    
    public virtual async Task<OperationTypeDTO> DeactivateOperationType(string id) {
        var operationType = await _repository.GetByIdAsync(new OperationTypeId(id));
        if (operationType == null) {
            throw new Exception("Operation Type not found.");
        }
        operationType.Status = Status.INACTIVE;
        _repository.Update(operationType);
        await _unitOfWork.CommitAsync();

        await _logRepository.AddAsync(new DomainLog(LogObjectType.OperationType, LogActionType.Edit, 
            string.Format("Deactivated Operation Type (Name = {0})", operationType.name.name)));
        
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

    public virtual async Task<OperationTypeDTO> UpdateOperationType([FromForm] UpdatedOperationTypeDTO dto, string id) {
        var operationType = this._repository.GetByIdAsync(new OperationTypeId(id)).Result;
        
        if (operationType == null) {
            throw new Exception("Operation Type not found.");
        }

        operationType.name = new OperationName(dto.name);
        operationType.anaesthesiaTime = new AnaesthesiaTime(dto.anaesthesiaTime);
        operationType.surgeryTime = new SurgeryTime(dto.surgeryTime);
        operationType.cleaningTime = new CleaningTime(dto.cleaningTime);
        
        _repository.Update(operationType);
        await _unitOfWork.CommitAsync();

        await _logRepository.AddAsync(new DomainLog(LogObjectType.OperationType, LogActionType.Edit, 
            string.Format("Updated Operation Type (Name = {0})", operationType.name.name)));

        return operationType.returnDTO();
    }

    public async Task<IEnumerable<OperationTypeDTO>> GetBySpecialization(string specialization) {
        var list = await _repository.GetAllAsync();
        return list.Where(type => type.Specialization.ToString() == specialization).Select(type => type.returnDTO());
    }

}
