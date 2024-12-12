using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.Shared;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Backend.Domain.DomainLogs;

namespace Backend.Domain.Specializations;

public class SpecializationService{

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

    public virtual async Task<SpecializationDTO> CreateSpecializationDTO([FromForm] SpecializationDTO dto) {
        
        Specialization specialization = Specialization.FromDTO(dto);
        
        await _repository.AddAsync(specialization);
        await _unitOfWork.CommitAsync();

        return specialization.ToDTO();
    }    
}