using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.DomainLogs;

public interface IDomainLogRepository : IRepository<DomainLog, DomainLogId> {


}