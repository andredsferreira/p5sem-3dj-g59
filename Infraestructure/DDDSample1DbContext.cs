using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Infrastructure.Auth;

namespace DDDSample1.Infrastructure {

    public class DDDSample1DbContext : DbContext {

        public virtual DbSet<OperationRequest> OperationRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Patient> Patients {get; set;}

        public DDDSample1DbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}