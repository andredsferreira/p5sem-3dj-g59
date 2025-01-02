using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Auth;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "DoctorPolicy")]
public class OperationRequestController : ControllerBase {

    private readonly OperationRequestService _service;

    public OperationRequestController(OperationRequestService service) {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOperationRequest([FromBody] CreateOperationRequestDTO dto) {
        try {
            var result = await _service.CreateOperationRequest(dto);
            return Ok(new { message = $"Successfully created operation request: {result}" });
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
    public async Task<IActionResult> UpdateOperationRequest([FromBody] UpdateOperationRequestDTO dto) {
        try {
            var result = await _service.UpdateOperationRequest(dto);
            return Ok(new { message = $"Successfully updated operation request: {result}" });
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
            var result = await _service.DeleteOperationRequest(id);
            return Ok(new {
                message = $"Successfully deleted operation request: {result}"
            });
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

