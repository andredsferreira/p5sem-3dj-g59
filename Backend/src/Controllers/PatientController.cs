using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Patients;
using Microsoft.AspNetCore.Authorization;
using DDDSample1.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Domain.Appointments;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Shared;
using System;

namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PatientController : ControllerBase {

    private readonly PatientService _service;


    private readonly UserManager<IdentityUser> userManager;

    public PatientController(PatientService service, UserManager<IdentityUser> userManager) {
        _service = service;
        this.userManager = userManager;
    }

    [HttpPost("Create")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<PatientDTO>> CreatePatient(PatientDTO dto) {
        var cat = await _service.CreatePatient(dto);
        return CreatedAtAction("Patient creation", cat);
    }
    [HttpPut("Edit/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<PatientDTO>> EditPatient(string id, [FromBody] PatientDTO dto) {
        throw new NotImplementedException();
    }

    [HttpDelete("Delete/{record}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<PatientDTO>> DeletePatient(string record) {
        try {
            Console.WriteLine("Received Record: "+record);
            var pat = await _service.DeletePatient(new MedicalRecordNumber(record));
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("Appointments")]
    [Authorize(Roles = HospitalRoles.Patient)]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetPatientAppointments(string patientEmail) {
        var appointments = await _service.GetPatientAppointments(patientEmail);
        return appointments.ToList();
    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients() {
        return await _service.GetAll();
    }
}
