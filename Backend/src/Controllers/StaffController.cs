using Backend.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System;
using Backend.Domain.Shared;
using Backend.Domain.Patients;


namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class StaffController : ControllerBase {

    private readonly StaffService _service;


    public StaffController(StaffService service) {
        _service = service;
    }






    [HttpPost("Create")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> CreateStaff(StaffDTO dto) {
        var cat = await _service.CreateStaff(dto);
        return  CreatedAtAction(nameof(GetStaffById), new { id = cat.LicenseNumber }, cat);

    }

   

    [HttpGet("Get/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public ActionResult<StaffDTO> GetStaffById(string id) {
        var cat = _service.GetStafftById(new LicenseNumber(id));
        if (cat == null) return NotFound();
        return Ok(cat);
    }



    [HttpPut("Edit/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> EditStaff(string id, [FromBody] FilterStaffDTO dto) {
        try {
            var pat = await _service.EditStaff(new LicenseNumber(id), dto);
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }



    [HttpDelete("Delete/{license}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<StaffDTO>> DeleteStaff(string license) {
        try {
            var pat = await _service.DeleteStaff(new LicenseNumber(license));
            if (pat == null) return NotFound(license);
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    
    [HttpPost("Search")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<StaffDTO>>> SearchAndFilterStaffs(FilterStaffDTO filterStaffDTO){
        var staff = await _service.SearchStaffs(filterStaffDTO);
        if (staff == null) return NotFound();
        return Ok(staff.ToList());
    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllStaffs() {
        var staff = await _service.GetAll();
        return staff.ToList();
    }


}
