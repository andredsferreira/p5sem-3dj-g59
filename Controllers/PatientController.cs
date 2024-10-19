using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDDSample1.Domain.Patients;

namespace DDDSample1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase {

    private readonly PatientService _service;
    public PatientController(PatientService service) {
        _service = service;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<PatientDTO>> CreatePatient(PatientDTO dto) {
        var cat = await _service.CreatePatient(dto);
        return CreatedAtAction("Patient creation", cat);
    }
}
