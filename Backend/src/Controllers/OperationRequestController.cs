using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain.OperationRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OperationRequestController : ControllerBase {

    private readonly OperationRequestService _service;

    public OperationRequestController(OperationRequestService service) {
        _service = service;
    }

    [HttpPost("create")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<OperationRequestDTO>> CreateOperationRequest([FromForm] OperationRequestDTO dto) {
        var createdOperationRequest = await _service.CreateOperationRequest(dto);
        return Ok(createdOperationRequest);
    }

    [HttpPut("update")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<UpdatedOperationRequestDTO>> UpdateOperationRequest([FromForm] UpdatedOperationRequestDTO dto) {
        var updatedOperationRequest = await _service.UpdateOperationRequest(dto);
        return CreatedAtAction("Updated operation request with ID: ", updatedOperationRequest.updatedId);
    }

    [HttpDelete("delete")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<Guid>> DeleteOperationRequest(Guid id) {
        var deletedRequestId = await _service.DeleteOperationRequest(id);
        return CreatedAtAction("Deleted operation request with ID: ", deletedRequestId);
    }

    [HttpGet("list")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<List<OperationRequestDTO>>> ListOperationRequests() {
        var operationRequests = await _service.ListOperationRequests();
        return operationRequests;
    }

}

