using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

namespace Backend.Domain.Slots;

public class SlotId : EntityId {


    [JsonConstructor]
    public SlotId(Guid value) : base(value) {

    }

    public SlotId(object value) : base(value) {
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