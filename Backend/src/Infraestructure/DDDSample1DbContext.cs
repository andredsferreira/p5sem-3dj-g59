using Microsoft.EntityFrameworkCore;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Infrastructure.OperationRequests;
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
using DDDSample1.Domain.Auth;
using System.Linq;

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

        SeedPatient(modelBuilder, new MedicalRecordNumber("202410000001"), new DateOnly(2001, 10, 21), "patientA@hospital.com", "910555111", Gender.Male, new FullName("João Camião"), new List<string>());
        SeedPatient(modelBuilder, new MedicalRecordNumber("202410000002"), new DateOnly(1998, 5, 14), "patientB@hospital.com", "910555222", Gender.Male, new FullName("Bruno Silva"), new List<string>());
        SeedPatient(modelBuilder, new MedicalRecordNumber("202410000003"), new DateOnly(1995, 12, 30), "patientC@hospital.com", "910555333", Gender.Female, new FullName("Carla Ferreira"), new List<string>());

        SeedStaff(modelBuilder, HospitalRoles.Admin);
        SeedStaff(modelBuilder, HospitalRoles.Doctor);
        SeedStaff(modelBuilder, HospitalRoles.Doctor);
        SeedStaff(modelBuilder, HospitalRoles.Nurse);
        SeedStaff(modelBuilder, HospitalRoles.Nurse);

        SeedOperationType(modelBuilder, "ACL Reconstruction");
        SeedOperationType(modelBuilder, "Knee Replacement");
        SeedOperationType(modelBuilder, "Shoulder Replacement");
        SeedOperationType(modelBuilder, "Hip Replacement");
        SeedOperationType(modelBuilder, "Meniscal Injury Treatment");

        base.OnModelCreating(modelBuilder);
    }

    private void SeedPatient(ModelBuilder builder, MedicalRecordNumber medicalRecordNumber, DateOnly dateOfBirth, string email, string phoneNumber, Gender gender, FullName fullName, List<string> allergies) {
        List<Allergy> allergiesAllergy = allergies
            .Select(allergyName => new Allergy(allergyName))
            .ToList();
        var patient = new Patient(medicalRecordNumber, dateOfBirth, email, phoneNumber, gender, fullName, allergiesAllergy);
        builder.Entity<Patient>().HasData(patient);
    }

    // TODO: Completar metodo
    private void SeedStaff(ModelBuilder builder, string role) {
        var staff = new Staff(role);
        builder.Entity<Staff>().HasData(staff);
    }

    private void SeedOperationType(ModelBuilder builder, string name) {
        var operationType = new OperationType(new OperationName(name));
        builder.Entity<OperationType>().HasData(operationType);
    }


    private void SeedOperationRequests() {

    }
}