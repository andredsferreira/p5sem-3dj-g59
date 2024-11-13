using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Domain.SurgeryRooms;


public class SurgeryRoomService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly ISurgeryRoomRepository _repository;

    public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository repository) {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public SurgeryRoomDTO GetRoomByNumber(RoomNumber Number){
        var room = this._repository.GetByNumber(Number);
        if (room == null) return null;
        return room.ReturnDTO();
    }

    public async virtual Task<SurgeryRoomDTO> CreatePatient(SurgeryRoomDTO dto) {
        var patient = SurgeryRoom.CreateFromDTO(dto);
        await this._repository.AddAsync(patient);
        await this._unitOfWork.CommitAsync();
        return dto;
    }

    public async Task<IEnumerable<SurgeryRoomDTO>> GetAll() {
        var list = _repository.GetAllAsync();
        return (await list).Select(room => room.ReturnDTO());
    }
}

