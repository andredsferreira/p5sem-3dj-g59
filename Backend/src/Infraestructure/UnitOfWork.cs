using System.Threading.Tasks;
using Backend.Domain.Shared;

namespace Backend.Infrastructure;
public class UnitOfWork : IUnitOfWork {

    private readonly AppDbContext context;

    private readonly AppDbContext identityContext;

    public UnitOfWork(AppDbContext context, AppDbContext identityContext) {
        this.context = context;
        this.identityContext = identityContext;
    }

    public async Task<int> CommitAsync() {
        using (var transaction = await context.Database.BeginTransactionAsync()) {
            try {
                var result1 = await context.SaveChangesAsync();
                var result2 = await identityContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return result1 + result2;
            }
            catch {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}