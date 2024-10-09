using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;

namespace DDDSample1.Infrastructure {

    public class DDDSample1DbContext : DbContext {

        public DbSet<OperationRequest> OperationRequests { get; set; }

        public DDDSample1DbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
        }
    }
}