using DDDSample1.Domain.OperationRequests;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class OperationRequestController : ControllerBase {

            private readonly OperationRequestService _service; 
            
    }

}