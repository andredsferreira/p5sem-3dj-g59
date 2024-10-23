using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Infrastructure {
    public class UnitOfWork : IUnitOfWork {

        private readonly DDDSample1DbContext context;

        private readonly IdentityContext identityContext;

        public UnitOfWork(DDDSample1DbContext context, IdentityContext identityContext) {
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
}