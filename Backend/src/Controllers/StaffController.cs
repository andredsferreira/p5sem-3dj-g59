using DDDSample1.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase {

    private readonly StaffService _service;

}
