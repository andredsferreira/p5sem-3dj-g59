using DDDSample1.Domain.Staffs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;


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
        return CreatedAtAction("Staff creation", cat);
    }

    /*[HttpPut("Edit/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<PatientDTO>> EditPatient(string id, [FromBody] FilterPatientDTO dto) {
        try {
            var pat = await _service.EditPatient(new MedicalRecordNumber(id), dto);
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpDelete("Delete/{record}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<PatientDTO>> DeletePatient(string record) {
        try {
            var pat = await _service.DeletePatient(new MedicalRecordNumber(record));
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("Search")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<IEnumerable<PatientDTO>>> SearchAndFilterPatients(FilterPatientDTO filterPatientDTO){
        var patients = await _service.SearchPatients(filterPatientDTO);
        return patients.ToList();
    }*/

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<StaffDTO>>> GetAllPatients() {
        var pats = await _service.GetAll();
        return pats.ToList();
    }

    

}
