using DDDSample1.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Auth;
using System.Threading.Tasks;


namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class StaffController : ControllerBase {

    private readonly StaffService _service;






    [HttpPost("Create")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> CreatePatient(StaffDTO dto) {
        var cat = await _service.CreateStaff(dto);
        return CreatedAtAction("Patient creation", cat);
    }


    

}
