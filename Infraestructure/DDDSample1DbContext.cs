using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Infrastructure.Auth;
using DDDSample1.Domain.OperationTypes;
using Microsoft.Extensions.Configuration;

namespace DDDSample1.Infrastructure {

    public class DDDSample1DbContext : DbContext {

        private readonly IConfiguration configuration;

        public virtual DbSet<OperationRequest> OperationRequests { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Patient> Patients { get; set; }

        public virtual DbSet<OperationType> OperationTypes { get; set; }

        public DDDSample1DbContext(IConfiguration configuration) {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionString = configuration.GetConnectionString("MySqlConnection");
            optionsBuilder.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}