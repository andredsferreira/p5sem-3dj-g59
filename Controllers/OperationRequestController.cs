using System;
using System.Threading.Tasks;
using DDDSample1.Domain.OperationRequests;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class OperationRequestController : ControllerBase {

        private readonly OperationRequestService _service;

        [HttpPost]
        public async Task<ActionResult<OperationRequestDTO>> CreateOperationRequest(OperationRequestDTO dto) {
            var cat = await _service.CreateOperationRequest(dto);
            throw new NotImplementedException();
        }

    }

}