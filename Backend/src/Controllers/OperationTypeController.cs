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
[Authorize]
public class OperationTypeController : ControllerBase {

    private readonly IConfiguration Configuration;

    private readonly AppDbContext Context;

    private readonly UserManager<IdentityUser> UserManager;
    
    private readonly AddOperationTypeService AddOperationTypeService;

    public OperationTypeController(AddOperationTypeService service) {
        this.AddOperationTypeService = service;
    }

    public OperationTypeController(IConfiguration Configuration, AppDbContext Context, UserManager<IdentityUser> UserManager, AddOperationTypeService AddOperationTypeService) {
        this.Configuration = Configuration;
        this.Context = Context;
        this.UserManager = UserManager;
        this.AddOperationTypeService = AddOperationTypeService;
    }

    [HttpPost("Add")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> AddOperationType([FromForm] OperationTypeDTO dto) {

        var result = await AddOperationTypeService.CreateOperationType(dto);

        return result != null ? Ok(result) : BadRequest("Could not create operation type");
    }



    private async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetOperationTypes() {
        var list = await AddOperationTypeService.GetAll();
        return list.ToList();

    }

    [HttpGet("All")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> DeactivateOperationType() {
        return await GetOperationTypes();
    }


    [HttpPut("Edit/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> UpdateOperationType([FromForm] UpdatedOperationTypeDTO operationTypeDTO, string id) {

        var result = await AddOperationTypeService.UpdateOperationType(operationTypeDTO, id);
        return result != null ? Ok(result) : BadRequest("Could not update operation type");

    }

    [HttpPut("Deactivate/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> DeactivateOperationType(String id) {

        var result = await AddOperationTypeService.DeactivateOperationType(id);
        return result != null ? Ok(result) : BadRequest("Could not deactivate operation type");
    }

    [HttpGet("Get/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> GetOperationType(String id) {

        var result = await AddOperationTypeService.GetOperationTypeById(id);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }

    [HttpGet("GetByName/{name}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> GetOperationTypeByName(string name) {

        var result = await AddOperationTypeService.GetOperationTypeByName(name);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }

    [HttpGet("GetStatus/{status}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetOperationTypeByStatus(string status) {

        var list = await AddOperationTypeService.GetAll();
        var result = list.Where(type => type.Status == status);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }

    [HttpGet("GetSpecialization/{specialization}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetOperationTypeBySpecialization(string specialization) {

        var result = await AddOperationTypeService.GetBySpecialization(specialization);
        return result != null ? Ok(result) : BadRequest("Could not find operation type");
    }



}