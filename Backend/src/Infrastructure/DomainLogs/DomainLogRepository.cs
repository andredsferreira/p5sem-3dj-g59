using Backend.Domain.DomainLogs;
using Backend.Infrastructure.Shared;

namespace Backend.Infrastructure.DomainLogs;

public class DomainLogRepository : BaseRepository<DomainLog, DomainLogId>, IDomainLogRepository {

    public DomainLogRepository(AppDbContext context) : base(context.DomainLogs) {
    }
    
}

