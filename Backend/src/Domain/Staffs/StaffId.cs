using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

namespace Backend.Domain.Staffs;

public class StaffId : EntityId {

    [JsonConstructor]
    public StaffId(Guid value) : base(value) {

    }

    public StaffId(object value) : base(value) {

    }

    public override string AsString() {
        Guid obj = (Guid)base.ObjValue;
        return obj.ToString();
    }

    protected override object createFromString(string text) {
        return new Guid(text);
    }

    public Guid AsGuid() {
        return (Guid)base.ObjValue;
    }

}