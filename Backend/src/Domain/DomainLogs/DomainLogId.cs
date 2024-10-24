using DDDSample1.Domain.Shared;
using System;
using System.Text.Json.Serialization;

namespace DDDSample1.Domain.DomainLogs;

public class DomainLogId : EntityId {
    [JsonConstructor]
    public DomainLogId(Guid value) : base(value) {

    }

    public DomainLogId(string value) : base(value) {

    }

    override
    protected object createFromString(string text) {
        return new Guid(text);
    }

    override
    public string AsString() {
        Guid obj = (Guid)base.ObjValue;
        return obj.ToString();
    }

    public Guid AsGuid() {
        return (Guid)base.ObjValue;
    }
}