using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

namespace Backend.Domain.Patients;

public class PatientId : EntityId {


    [JsonConstructor]
    public PatientId(Guid value) : base(value) {

    }

    public PatientId(object value) : base(value) {
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