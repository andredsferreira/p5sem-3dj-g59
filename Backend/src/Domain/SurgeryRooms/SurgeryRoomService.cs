using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Appointments;
using Backend.Domain.OperationRequests;
using Backend.Domain.OperationTypes;
using Backend.Domain.Shared;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Domain.SurgeryRooms;


public class SurgeryRoomService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly ISurgeryRoomRepository _repository;

    private readonly IAppointmentRepository _aprepo;

    private readonly IOperationRequestRepository _opreqrepo;

    private readonly IOperationTypeRepository _optyperepo;

    public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository repository, IAppointmentRepository aprepo, IOperationRequestRepository opreqrepo, IOperationTypeRepository optyperepo) {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _aprepo = aprepo;
        _opreqrepo = opreqrepo;
        _optyperepo = optyperepo;
    }

    public async Task<SurgeryRoomDTO> GetRoomByNumber(RoomNumber Number){
        var room = await this._repository.GetByNumber(Number);
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
        var room = this._repository.GetByNumber(roomNumber) ?? throw new KeyNotFoundException("Room does not exist");
        Console.WriteLine(room.Id);
        var appointments = (await _aprepo.GetAllAsync()).Where(a => a.SurgeryRoomId.Equals(room.Id));
        if (appointments.IsNullOrEmpty()) return false;
        foreach(Appointment ap in appointments) {
            var optype = await _optyperepo.GetByIdAsync((await _opreqrepo.GetByIdAsync(ap.OperationRequestId)).operationTypeId);
            var opTime = optype.anaesthesiaTime.duration + optype.surgeryTime.duration + optype.cleaningTime.duration;
            if (time.CompareTo(ap.DateTime) > 0 && time.CompareTo(ap.DateTime.AddMinutes(opTime)) < 0)
                return true;
        }
        return false;
    }
}

