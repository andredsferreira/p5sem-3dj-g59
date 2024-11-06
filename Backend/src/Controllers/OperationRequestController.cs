using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = HospitalRoles.Doctor)]
public class OperationRequestController : ControllerBase {

    private readonly OperationRequestService _service;

    public OperationRequestController(OperationRequestService service) {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOperationRequest([FromBody] OperationRequestDTO dto) {
        try {
            var createdOperationRequest = await _service.CreateOperationRequest(dto);
            return Ok(createdOperationRequest);
        }
        catch (PatientNotFoundException ex) {
            return BadRequest(new { error = ex.Message });
        }
        catch (EmptyUserNameException ex) {
            return BadRequest(new { error = ex.Message });
        }
        catch (StaffNotRegisteredException ex) {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationTypeException ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateOperationRequest([FromBody] UpdatedOperationRequestDTO dto) {
        try {
            var updatedOperationRequest = await _service.UpdateOperationRequest(dto);
            return Ok(updatedOperationRequest);
        }
        catch (OperationRequestNotFoundException ex) {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationRequestException ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteOperationRequest(Guid id) {
        try {
            var deletedRequestId = await _service.DeleteOperationRequest(id);
            return Ok();
        }
        catch (OperationRequestNotFoundException ex) {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationRequestException ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListOperationRequests() {
        var operationRequests = await _service.ListOperationRequests();
        return operationRequests.Any() ? Ok(operationRequests)
            : NotFound("No operation requests were found");
    }

}

