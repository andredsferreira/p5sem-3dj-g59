using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

namespace Backend.Domain.OperationRequests;

public class OperationRequestId : EntityId {

    [JsonConstructor]
    public OperationRequestId(Guid value) : base(value) {

    }

    public OperationRequestId(string value) : base(value) {

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