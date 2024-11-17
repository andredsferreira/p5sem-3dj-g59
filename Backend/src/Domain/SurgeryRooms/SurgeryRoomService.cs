using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Appointments;
using Backend.Domain.Shared;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Domain.SurgeryRooms;


public class SurgeryRoomService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly ISurgeryRoomRepository _repository;

    private readonly IAppointmentRepository _aprepo;

    public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository repository, IAppointmentRepository aprepo) {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _aprepo = aprepo;
    }

    public SurgeryRoomDTO GetRoomByNumber(RoomNumber Number){
        var room = this._repository.GetByNumber(Number);
        if (room == null) return null;
        return room.ReturnDTO();
    }

    public async virtual Task<SurgeryRoomDTO> CreateRoom(SurgeryRoomDTO dto) {
        var room = SurgeryRoom.CreateFromDTO(dto);
        await this._repository.AddAsync(room);
        await this._unitOfWork.CommitAsync();
        return dto;
    }

    public async Task<IEnumerable<SurgeryRoomDTO>> GetAll() {
        var list = _repository.GetAllAsync();
        return (await list).Select(room => room.ReturnDTO());
    }
    public async Task<bool> IsRoomOccupiedAsync(RoomNumber roomNumber, DateTime time){
        var room = GetRoomByNumber(roomNumber) ?? throw new KeyNotFoundException("Room does not exist");
        var appointments = (await _aprepo.GetAllAsync()).Where(a => a.SurgeryRoom.Equals(room));
        if (appointments.IsNullOrEmpty()) return false;
        foreach(Appointment ap in appointments)
            if (time.CompareTo(ap.DateTime) > 0 && time.CompareTo(ap.EndDateTime()) < 0)
                return true;
        return false;
    }
}

