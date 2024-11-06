using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Auth;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[Authorize(Roles = HospitalRoles.Doctor)]
[ApiController]

public class OperationRequestController : ControllerBase {

    private readonly OperationRequestService _service;

    public OperationRequestController(OperationRequestService service) {
        _service = service;
    }

    [HttpPost("create")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<IActionResult> CreateOperationRequest([FromBody] CreateOperationRequestDTO dto) {
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
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<IActionResult> UpdateOperationRequest([FromBody] UpdateOperationRequestDTO dto) {
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
    [Authorize(Roles = HospitalRoles.Doctor)]
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
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<IActionResult> ListOperationRequests() {
        var operationRequests = await _service.ListOperationRequests();
        return operationRequests.Any() ? Ok(operationRequests)
            : NotFound("No operation requests were found");
    }

}

