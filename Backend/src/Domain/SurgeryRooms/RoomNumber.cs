using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

namespace Backend.Domain.SurgeryRooms;

public class RoomNumber : IValueObject {
    public int Number;
    public RoomNumber(int Number){this.Number = Number;}
}
