using System;
using Backend.Domain.Shared;
using System.Text.Json.Serialization;

namespace Backend.Domain.Specializations;

public class SpecializationId : EntityId {

    [JsonConstructor]
    public SpecializationId(Guid value) : base(value) { }

    public SpecializationId(object value) : base(value) { }

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