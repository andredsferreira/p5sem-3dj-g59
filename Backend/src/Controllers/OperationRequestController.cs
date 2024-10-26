using System;
using System.Collections.Generic;
using System.Linq;
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
        return createdOperationRequest != null ? Ok(createdOperationRequest) : BadRequest("Could not create operation request");
    }

    [HttpPut("update")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<UpdatedOperationRequestDTO>> UpdateOperationRequest([FromForm] UpdatedOperationRequestDTO dto) {
        var updatedOperationRequest = await _service.UpdateOperationRequest(dto);
        return updatedOperationRequest != null ? Ok(updatedOperationRequest) : BadRequest("Could not update operation request");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<Guid>> DeleteOperationRequest(Guid id) {
        var deletedRequestId = await _service.DeleteOperationRequest(id);
        return deletedRequestId != null ? Ok(deletedRequestId) : BadRequest("Could not delete operation request");
    }

    [HttpGet("list")]
    [Authorize(Roles = HospitalRoles.Doctor)]
    public async Task<ActionResult<List<OperationRequestDTO>>> ListOperationRequests() {
        var operationRequests = await _service.ListOperationRequests();
        return operationRequests.Any() ? Ok(operationRequests) : NotFound("No operation requests were found");
    }

}

