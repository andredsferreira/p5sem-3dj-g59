using System;
using System.Text.Json.Serialization;
using Backend.Domain.Shared;

namespace Backend.Domain.OperationTypes;

public class OperationTypeId : EntityId {

    [JsonConstructor]
    public OperationTypeId(Guid value) : base(value) { }

    public OperationTypeId(object value) : base(value) { }
    
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
