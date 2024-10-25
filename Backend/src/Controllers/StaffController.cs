using DDDSample1.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class StaffController : ControllerBase {

    private readonly StaffService _service;
    private readonly UserManager<IdentityUser> userManager;


    public StaffController(StaffService service,UserManager<IdentityUser> userManager) {
        _service = service;
        this.userManager= userManager;
    }






    [HttpPost("Create")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> CreateStaff(StaffDTO dto) {
        var cat = await _service.CreateStaff(dto);
        return CreatedAtAction("Staff creation", cat);
    }

    /*[HttpPut("Edit/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> EditStaff(string id, [FromBody] FilterStaffDTO dto) {
        try {
            var pat = await _service.EditStaff(new MedicalRecordNumber(id), dto);
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpDelete("Delete/{record}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> DeleteStaff(string record) {
        try {
            var pat = await _service.DeleteStaff(new MedicalRecordNumber(record));
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("Search")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<StaffDTO>>> SearchAndFilterStaffs(FilterStafftDTO filteStaffDTO){
        var staff = await _service.SearchStaffs(filterStaffDTO);
        return staff.ToList();
    }*/

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaffs() {
        var pats = await _service.GetAll();
        return pats.ToList();
    }

    

}
