using System.Threading.Tasks;
using Backend.Domain.Auth;
using Backend.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Backend.Domain.OperationTypes;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Policy = "AdminPolicy, DoctorPolicy")]
public class OperationTypeController : ControllerBase {


    private readonly AddOperationTypeService AddOperationTypeService;

    public OperationTypeController(AddOperationTypeService AddOperationTypeService) {
        this.AddOperationTypeService = AddOperationTypeService;
    }

    [HttpPost("Add")]
    public async Task<ActionResult<OperationTypeDTO>> AddOperationType([FromForm] OperationTypeDTO dto) {

        var result = await AddOperationTypeService.CreateOperationType(dto);

        return result != null ? Ok(result) : BadRequest("Could not create operation type");
    }



    private async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetOperationTypes() {
        var list = await AddOperationTypeService.GetAll();
        return list.ToList();

    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> DeactivateOperationType() {
        return await GetOperationTypes();
    }


    [HttpPut("Edit/{id}")]
    public async Task<ActionResult<OperationTypeDTO>> UpdateOperationType([FromForm] UpdatedOperationTypeDTO operationTypeDTO, string id) {

        var result = await AddOperationTypeService.UpdateOperationType(operationTypeDTO, id);
        return result != null ? Ok(result) : BadRequest("Could not update operation type");

    }

    [HttpDelete("Deactivate/{id}")]
    public async Task<ActionResult<OperationTypeDTO>> DeactivateOperationType(String id) {

        var result = await AddOperationTypeService.DeactivateOperationType(id);
        return result != null ? Ok(result) : BadRequest("Could not deactivate operation type");
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<OperationTypeDTO>> GetOperationType(String id) {

        var result = await AddOperationTypeService.GetOperationTypeById(id);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }

    [HttpGet("GetByName/{name}")]
    public async Task<ActionResult<OperationTypeDTO>> GetOperationTypeByName(string name) {

        var result = await AddOperationTypeService.GetOperationTypeByName(name);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }

    [HttpGet("GetStatus/{status}")]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetOperationTypeByStatus(string status) {

        var list = await AddOperationTypeService.GetAll();
        var result = list.Where(type => type.Status == status);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }

    /*[HttpGet("GetSpecialization/{specialization}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetOperationTypeBySpecialization(string specialization) {

        var result = await AddOperationTypeService.GetBySpecialization(specialization);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }*/



}