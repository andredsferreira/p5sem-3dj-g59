using System.Threading.Tasks;
using Backend.Domain.Shared;

namespace Backend.Infrastructure;
public class UnitOfWork : IUnitOfWork {

    private readonly AppDbContext context;


    public UnitOfWork(AppDbContext context) {
        this.context = context;
    }

    public async Task<int> CommitAsync() {
        using (var transaction = await context.Database.BeginTransactionAsync()) {
            try {
                var result1 = await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result1;
            }
            catch {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}