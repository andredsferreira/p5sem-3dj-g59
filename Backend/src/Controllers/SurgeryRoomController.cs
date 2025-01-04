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
using System;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SurgeryRoomController : ControllerBase {

    private readonly SurgeryRoomService _service;

    public SurgeryRoomController(SurgeryRoomService service) {
        _service = service;
    }

    [HttpPost("Create")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<SurgeryRoomDTO>> CreateRoom(SurgeryRoomDTO dto) {
        var cat = await _service.CreateRoom(dto);
        return CreatedAtAction(nameof(GetRoomByNumber), new { id = cat.Number }, cat);
    }

    [HttpGet("Get/{id}")]
    [Authorize(Roles = HospitalRoles.Admin)]
    public async Task<ActionResult<SurgeryRoomDTO>> GetRoomByNumber(int id) {
        var cat = await _service.GetRoomByNumber(new RoomNumber(id));
        if (cat == null) return NotFound();
        return Ok(cat);
    }

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<SurgeryRoomDTO>>> GetAllPatients() {
        var pats = await _service.GetAll();
        return pats.ToList();
    }

    [HttpGet("Occupied/{id}/{date}")]
    public async Task<ActionResult<OccupiedDTO>> IsRoomOccupied(int id, string date){
        OccupiedDTO result;
        try{
            result = await _service.IsRoomOccupiedAsync(new RoomNumber(id), DateTime.ParseExact(date, "yyyyMMddHHmm", null));
        }
        catch (KeyNotFoundException){
            return NotFound();
        }
        return Ok(result);
    }
}
