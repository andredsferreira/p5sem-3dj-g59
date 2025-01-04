using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Appointments;
using Backend.Domain.Auth;
using Backend.Domain.OperationRequests;
using Backend.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "DoctorPolicy")]
public class AppointmentController : ControllerBase {

    private readonly AppointmentService _service;

    public AppointmentController(AppointmentService service) {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO dto) {
        try {
            var result = await _service.CreateAppointment(dto);
            return Ok(new { message = $"Successfully created appointment: {result}" });
        }
        catch (EmptyUserNameException ex) {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationRequestException ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDTO dto) {
        try {
            var result = await _service.UpdateAppointment(dto);
            return Ok(new { message = $"Successfully updated appointment: {result}" });
        }
        catch (Exception ex) {
            return BadRequest(new { error = ex.Message });
        }
    }

    /*[HttpDelete("delete")]
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
    }*/

    /*[HttpGet("list")]
    public async Task<IActionResult> ListOperationRequests() {
        var operationRequests = await _service.ListOperationRequests();
        return operationRequests.Any() ? Ok(operationRequests)
            : NotFound("No operation requests were found");
    }*/

}

