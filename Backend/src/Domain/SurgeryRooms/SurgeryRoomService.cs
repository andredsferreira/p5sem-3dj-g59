using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Appointments;
using Backend.Domain.OperationRequests;
using Backend.Domain.OperationTypes;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Domain.Appointments;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Domain.SurgeryRooms;


public class SurgeryRoomService {

    private readonly IUnitOfWork _unitOfWork;

    private readonly ISurgeryRoomRepository _repository;

    private readonly IAppointmentRepository _aprepo;

    private readonly IOperationRequestRepository _opreqrepo;

    private readonly IOperationTypeRepository _optyperepo;
    
    private readonly IPatientRepository _patrepo;

    public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository repository, IAppointmentRepository aprepo, IOperationRequestRepository opreqrepo, IOperationTypeRepository optyperepo, IPatientRepository patrepo) {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _aprepo = aprepo;
        _opreqrepo = opreqrepo;
        _optyperepo = optyperepo;
        _patrepo = patrepo;
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
    public async Task<OccupiedDTO> IsRoomOccupiedAsync(RoomNumber roomNumber, DateTime time){
        var room = await this._repository.GetByNumber(roomNumber) ?? throw new KeyNotFoundException("Room does not exist");
        Console.WriteLine(room.Id);

        if(room.IsInMaintenance(time)) return new OccupiedDTO {Status = RoomStatus.UnderMaintenance};

        var appointments = (await _aprepo.GetAllAsync()).Where(a => a.SurgeryRoomId.Equals(room.Id));
        if (appointments.IsNullOrEmpty()) return new OccupiedDTO {
            RoomNumber = roomNumber.Number,
            Status = RoomStatus.Available
            };
        foreach(Appointment ap in appointments) {
            var optype = await _optyperepo.GetByIdAsync((await _opreqrepo.GetByIdAsync(ap.OperationRequestId)).operationTypeId);
            var opTime = optype.anaesthesiaTime.duration + optype.surgeryTime.duration + optype.cleaningTime.duration;
            if (time.CompareTo(ap.DateTime) > 0 && time.CompareTo(ap.DateTime.AddMinutes(opTime)) < 0){
                var opreq = await this._opreqrepo.GetByIdAsync(ap.OperationRequestId);
                var pat = await this._patrepo.GetByIdAsync(opreq.patientId);
                return new OccupiedDTO {
                    RoomNumber = roomNumber.Number,
                    Begin = ap.DateTime,
                    End = ap.DateTime.AddMinutes(opTime),
                    Status = RoomStatus.Occupied,
                    PatientName = pat.FullName.ToString(),
                    PatientMRN = pat.MedicalRecordNumber.ToString()
                };
            }
        }
        return new OccupiedDTO {
            RoomNumber = roomNumber.Number,
            Status = RoomStatus.Available
            };
    }
}

