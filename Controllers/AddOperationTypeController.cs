using System.Threading.Tasks;
using DDDSample1.Domain;
using DDDSample1.Domain.Auth;
using DDDSample1.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationTypes;


namespace DDDSample1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AddOperationTypeController : ControllerBase
{

    private readonly IConfiguration Configuration;
    private readonly IdentityContext Context;
    private readonly UserManager<IdentityUser> UserManager;
    private readonly AddOperationTypeService AddOperationTypeService;

    public AddOperationTypeController(IConfiguration Configuration, IdentityContext Context, UserManager<IdentityUser> UserManager, AddOperationTypeService AddOperationTypeService)
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

}