using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.Shared;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Backend.Domain.DomainLogs;

namespace Backend.Domain.Specializations;

public class SpecializationService{
/*
    private readonly ISpecializationRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IDomainLogRepository _logRepository;

    public SpecializationService(ISpecializationRepository repository, IUnitOfWork unitOfWork, IDomainLogRepository logRepository) {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logRepository = logRepository;
    }

    public SpecializationService() {
    }
/*
    public virtual async Task<SpecializationDTO> CreateSpecializationDTO([FromForm] SpecializationDTO dto) {
        
        Specialization specialization = Specialization.FromDTO(dto);
        
        await _repository.AddAsync(specialization);
        await _unitOfWork.CommitAsync();

        return specialization.ToDTO();
    }
    public virtual async Task<SpecializationDTO> GetSpecializationDTO() {
        var specializations = await _repository.GetAllAsync();
        return specializations.Select(s => s.ToDTO()).FirstOrDefault();
    }

    public virtual async Task<SpecializationDTO> editSpecialization(SpecializationDTO dto, string id){
        var specialization = this._repository.GetByIdAsync(new SpecializationId(id)).Result;
        
        if(specialization == null)
            throw new Exception("Specialization not found.");

        
        specialization.description = new Description(dto.description);
        specialization.designation = new Designation(dto.designation);
        specialization.codeSpec = new CodeSpec(dto.codeSpec);

        _repository.Update(specialization);
        await _unitOfWork.CommitAsync();

        return specialization.ToDTO(); 
    }    
*/
}