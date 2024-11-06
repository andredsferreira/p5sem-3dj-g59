namespace Backend.Domain.Appointments;

using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

public class AppointmentId : EntityId {


    [JsonConstructor]
    public AppointmentId(Guid value) : base(value) {

    }

    public AppointmentId(object value) : base(value) {
    }

    override
    protected Object createFromString(String text) {
        return new Guid(text);
    }

    override
    public String AsString() {
        Guid obj = (Guid)base.ObjValue;
        return obj.ToString();
    }


    public Guid AsGuid() {
        return (Guid)base.ObjValue;
    }
}