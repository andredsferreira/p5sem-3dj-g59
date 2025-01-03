using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Domain.Patients;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Domain.Appointments;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Shared;
using System.Net.Mail;
using System;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase {

    private readonly PatientService _service;



    public PatientController(PatientService service) {
        _service = service;
    }

    [HttpPost("Create")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<PatientDTO>> CreatePatient(PatientDTO dto) {
        var cat = await _service.CreatePatient(dto);
        return CreatedAtAction(nameof(GetPatientById), new { id = cat.MedicalRecordNumber }, cat);
    }

    [HttpGet("Get/{id}")]
    [Authorize(Policy = "AdminPolicy, DoctorPolicy")]
    public ActionResult<PatientDTO> GetPatientById(string id) {
        var cat = _service.GetPatientByIdAsync(new MedicalRecordNumber(id));
        if (cat == null) return NotFound();
        return Ok(cat);
    }

    [HttpPatch("Edit/{id}")]
    [Authorize(Policy = "AdminPolicy")]
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

#nullable disable
    [HttpPut("Edit/Self/{id}")]
    [Authorize(Policy = "PatientPolicy")]
    public async Task<ActionResult<PatientDTO>> EditSelf(string id, [FromBody] FilterPatientDTO dto) {
        try {
            var pat = await _service.EditPatientSelf(new MedicalRecordNumber(id), dto);
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }
#nullable restore

    [HttpPut("confirmEdit/id={id}&name={name}&email={email}&phone={phone}")]
    [Authorize(Policy = "PatientPolicy")]
    public async Task<ActionResult<PatientDTO>> ConfirmEdit(string id, string name, string email, string phone) {
        try {
            var pat = await _service.ConfirmEdit(id, name, email, phone);
            if (pat == null) return NotFound();
            return Ok(pat);
        }
        catch (BusinessRuleValidationException ex) {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpDelete("Delete/{record}")]
    [Authorize(Policy = "AdminPolicy")]
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

    [HttpPost("Search")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<IEnumerable<PatientDTO>>> SearchAndFilterPatients(FilterPatientDTO filterPatientDTO) {
        var patients = await _service.SearchPatients(filterPatientDTO);
        if (patients == null) return NotFound();
        return Ok(patients.ToList());
    }

    [HttpGet("Appointments")]
    [Authorize(Policy = "PatientPolicy")]
    public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetPatientAppointments(string patientEmail) {
        var appointments = await _service.GetPatientAppointments(patientEmail);
        return appointments.ToList();
    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<PatientDTO>>> GetAllPatients() {
        var pats = await _service.GetAll();
        return pats.ToList();
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetPatientByUserEmail(string email) {
        try {
            var mailAddress = new MailAddress(email);
            var patient = await _service.GetPatientByUserEmail(mailAddress);
            return patient != null
                ? Ok(patient)
                : NotFound("Patient not found");
        }
        catch (FormatException) {
            return BadRequest("Invalid email format.");
        }
    }

}
