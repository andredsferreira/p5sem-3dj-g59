using System;
using System.Threading.Tasks;
using Backend.Domain.Auth;

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

    [HttpPost("Add")]
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