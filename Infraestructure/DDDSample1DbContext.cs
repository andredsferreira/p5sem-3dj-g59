using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;
using DDDSample1.Domain.Auth;
using DDDSample1.Domain.Patients;
using DDDSample1.Infrastructure.Patients;
using DDDSample1.Domain.OperationTypes;
using Microsoft.Extensions.Configuration;
using DDDSample1.Infrastructure.OperationTypes;
using DDDSample1.Infrastructure.Staffs;
using System;
using System.Collections.Generic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;

namespace DDDSample1.Infrastructure;

public class DDDSample1DbContext : DbContext {

    private readonly IConfiguration configuration;

    public virtual DbSet<OperationRequest> OperationRequests { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<OperationType> OperationTypes { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public DDDSample1DbContext(IConfiguration configuration) {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString), options => options.SchemaBehavior(Pomelo.EntityFrameworkCore.MySql.Infrastructure.MySqlSchemaBehavior.Ignore));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());

        // Seeding patients
        SeedPatient(modelBuilder, new DateOnly(2001, 10, 21), "patientA@hospital.com", "910555111", new FullName("João Camião"), new List<string>());
        SeedPatient(modelBuilder, new DateOnly(1998, 5, 14), "patientB@hospital.com", "910555222", new FullName("Bruno Silva"), new List<string>());
        SeedPatient(modelBuilder, new DateOnly(1995, 12, 30), "patientC@hospital.com", "910555333", new FullName("Carla Ferreira"), new List<string>());

        // Seeding staff


        base.OnModelCreating(modelBuilder);
    }

    private void SeedPatient(ModelBuilder builder, DateOnly dateOfBirth, string email, string phoneNumber, FullName fullName, List<string> allergies) {
        var patient = new Patient(dateOfBirth, email, phoneNumber, fullName, allergies);
        builder.Entity<Patient>().HasData(patient);
    }


    private void SeedOperationRequests() {

    }
}