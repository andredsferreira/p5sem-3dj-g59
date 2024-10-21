using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Staffs;

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
        return (Guid)base.ObjValue;
    }

    public Guid AsGuid() {
        return (Guid)base.ObjValue;
    }

}