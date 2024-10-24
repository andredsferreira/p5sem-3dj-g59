using DDDSample1.Domain.DomainLogs;
using DDDSample1.Infrastructure.Shared;

namespace DDDSample1.Infrastructure.DomainLogs;

public class DomainLogRepository : BaseRepository<DomainLog, DomainLogId>, IDomainLogRepository {

    public DomainLogRepository(DDDSample1DbContext context) : base(context.DomainLogs) {
    }
    
}

