using System.Threading.Tasks;
using DDDSample1.Domain.Auth;
using DDDSample1.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DDDSample1.Domain.OperationTypes;
using System.Collections.Generic;


using System;

namespace DDDSample1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OperationTypeController : ControllerBase
{

    private readonly IConfiguration Configuration;
    private readonly IdentityContext Context;
    private readonly UserManager<IdentityUser> UserManager;
    private readonly AddOperationTypeService AddOperationTypeService;

    public OperationTypeController(IConfiguration Configuration, IdentityContext Context, UserManager<IdentityUser> UserManager, AddOperationTypeService AddOperationTypeService)
    {
        this.Configuration = Configuration;
        this.Context = Context;
        this.UserManager = UserManager;
        this.AddOperationTypeService = AddOperationTypeService;
    }

    [HttpPost("addoperationtype")]
    [Authorize(Roles = HospitalRoles.Admin)]

    public async Task<ActionResult<OperationTypeDTO>> AddOperationType(OperationTypeDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await AddOperationTypeService.CreateOperationType(dto.name, dto.anaesthesiaTime, dto.surgeryTime, dto.cleaningTime);
        
        return CreatedAtAction("Operation Type created with ID: ", result.id);
    }

    [HttpGet("getoperationtypes")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<OperationTypeDTO>> > GetOperationTypes()
    {
        //return await AddOperationTypeService.GetAll();
        throw new NotImplementedException();
        
    }

    [HttpPut("update/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> UpdateOperationType(OperationTypeDTO operationTypeDTO, string field, string value){

        throw new NotImplementedException();
    }

    [HttpPut("deactivate/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<OperationTypeDTO>> DeactivateOperationType(OperationTypeDTO operationTypeDTO){

        throw new NotImplementedException();
    }


}