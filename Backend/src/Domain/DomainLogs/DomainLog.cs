using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.DomainLogs;

public class DomainLog : Entity<DomainLogId> {
    public LogObjectType ObjectType { get; set; }
    public LogActionType ActionType { get; set; }
    public string Message { get; set; }

    public DomainLog(LogObjectType ObjectType, LogActionType ActionType, string Message){
        Id = new DomainLogId(Guid.NewGuid());
        this.ObjectType = ObjectType;
        this.ActionType = ActionType;
        this.Message = Message;
    }
}