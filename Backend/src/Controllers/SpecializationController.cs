using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Auth;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Specializations;


namespace Backend.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = HospitalRoles.Admin)]
[ApiController]

public class SpecializationController : ControllerBase{

    private readonly SpecializationService _specializationService;

    public SpecializationController(SpecializationService specializationService){
        _specializationService = specializationService;
    }

    [HttpPost]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<IActionResult> CreateSpecialization([FromForm] SpecializationDTO dto){
        try{
            var specialization = await _specializationService.CreateSpecializationDTO(dto);
            return Ok(specialization);
        }catch(Exception e){
            return BadRequest(e.Message);
        }
    }
    

}