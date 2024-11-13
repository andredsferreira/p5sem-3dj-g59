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
using Backend.Domain.SurgeryRooms;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SurgeryRoomController : ControllerBase {

    private readonly SurgeryRoomService _service;


    private readonly UserManager<IdentityUser> userManager;

    public SurgeryRoomController(SurgeryRoomService service, UserManager<IdentityUser> userManager) {
        _service = service;
        this.userManager = userManager;
    }

    [HttpPost("Create")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<SurgeryRoomDTO>> CreatePatient(SurgeryRoomDTO dto) {
        var cat = await _service.CreatePatient(dto);
        return CreatedAtAction(nameof(GetRoomByNumber), new { id = cat.Number }, cat);
    }

    [HttpGet("Get/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public ActionResult<PatientDTO> GetRoomByNumber(int id) {
        var cat = _service.GetRoomByNumber(new RoomNumber(id));
        if (cat == null) return NotFound();
        return Ok(cat);
    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<SurgeryRoomDTO>>> GetAllPatients() {
        var pats = await _service.GetAll();
        return pats.ToList();
    }
}
