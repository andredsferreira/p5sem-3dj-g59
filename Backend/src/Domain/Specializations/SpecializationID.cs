using System;
using Backend.Domain.Shared;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Backend.Domain.Specializations;

public class SpecializationID : EntityId {

    [JsonConstructor]
    public SpecializationID(Guid value) : base(value) { }

    public SpecializationID(object value) : base(value) { }

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