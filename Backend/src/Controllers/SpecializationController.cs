using System;
using System.Threading.Tasks;
using Backend.Domain.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Specializations;



namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]


public class SpecializationController : ControllerBase{

    private readonly SpecializationService specializationService;

    public SpecializationController(SpecializationService specializationService){
        this.specializationService = specializationService;
    }

    [HttpPost("Add")]
    public async Task<IActionResult> CreateSpecialization([FromForm] SpecializationDTO dto){
        try{
            var specialization = await specializationService.CreateSpecializationDTO(dto);
            return Ok(specialization);
        }catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetSpecialization(){
        try{
            var specialization = await specializationService.GetSpecializationDTO();
            return Ok(specialization);
        }catch(Exception e){
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("Edit")]
    public async Task<IActionResult> editSpecialization([FromForm] SpecializationDTO dto, string id){
        
        var result = await specializationService.editSpecialization(dto, id);
        return result != null ? Ok(result) : BadRequest("Could not update operation type");


    }


    

}