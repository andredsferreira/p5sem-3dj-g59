using Microsoft.EntityFrameworkCore;
using Backend.Domain.OperationRequests;
using Backend.Infrastructure.OperationRequests;
using Backend.Domain.Patients;
using Backend.Infrastructure.Patients;
using Backend.Domain.OperationTypes;
using Backend.Infrastructure.OperationTypes;
using Backend.Infrastructure.Staffs;
using System;
using System.Collections.Generic;
using Backend.Domain.Shared;
using Backend.Domain.Staffs;
using Backend.Domain.Auth;
using System.Linq;
using Backend.Domain.DomainLogs;
using Backend.Infrastructure.DomainLogs;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Backend.Infrastructure;

public class AppDbContext : IdentityDbContext<IdentityUser> {

    public virtual DbSet<OperationRequest> OperationRequests { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<OperationType> OperationTypes { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<DomainLog> DomainLogs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        IdentityBootstrap(modelBuilder);

        modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DomainLogEntityTypeConfiguration());

        var patientA = new Patient(
            new MedicalRecordNumber("202410000001"),
            new DateOnly(2001, 10, 21),
            new MailAddress("patientA@hospital.com"),
            new PhoneNumber("910555111"),
            Gender.Male,
            new FullName("João Camião"),
            new List<Allergy>());

        var patientB = new Patient(
            new MedicalRecordNumber("202410000002"),
            new DateOnly(1998, 5, 14),
            new MailAddress("patientB@hospital.com"),
            new PhoneNumber("910555222"),
            Gender.Male,
            new FullName("Bruno Silva"),
            new List<Allergy>());

        var patientC = new Patient(
            new MedicalRecordNumber("202410000003"),
            new DateOnly(1995, 12, 30),
            new MailAddress("patientC@hospital.com"),
            new PhoneNumber("910555333"),
            Gender.Female,
            new FullName("Carla Ferreira"),
            new List<Allergy>());

        var staffDoctor = new Staff(
            HospitalRoles.Doctor,
            "doctor");

        var staffDoctor2 = new Staff(
            HospitalRoles.Doctor,
            "doctor2",
            new MailAddress("doctor2@hospital.com"),
            new PhoneNumber("910555444"),
            new FullName("Doctor 2"),
            new LicenseNumber("f47ac10b-58cc-4372-a567-0e02b2c3d479"));

        var staffNurse = new Staff(
            HospitalRoles.Nurse,
            "nurse");

        var operationTypeA = new OperationType(new OperationName("ACL Reconstruction"));
        var operationTypeB = new OperationType(new OperationName("Knee Replacement"));
        var operationTypeC = new OperationType(new OperationName("Shoulder Replacement"));

        modelBuilder.Entity<Patient>().HasData(
            patientA,
            patientB,
            patientC);

        modelBuilder.Entity<Staff>().HasData(
            staffDoctor,
            staffDoctor2,
            staffNurse);


        modelBuilder.Entity<OperationType>().HasData(
            operationTypeA,
            operationTypeB,
            operationTypeC);

        SeedOperationRequest(modelBuilder,
            staffDoctor,
            patientA,
            operationTypeA,
            OperationRequestPriority.Elective,
            DateTime.Now,
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor,
            patientB,
            operationTypeB,
            OperationRequestPriority.Urgent,
            DateTime.Now,
            OperationRequestStatus.Pending);


        base.OnModelCreating(modelBuilder);
    }

    private void SeedOperationRequest(ModelBuilder builder, Staff doctor, Patient patient, OperationType operationType, OperationRequestPriority priority, DateTime dateTime, OperationRequestStatus requestStatus) {
        var operationRequest = new OperationRequest {
            Id = new OperationRequestId(Guid.NewGuid()),
            staffId = doctor.Id,
            patientId = patient.Id,
            operationTypeId = operationType.Id,
            priority = priority,
            dateTime = dateTime,
            requestStatus = requestStatus
        };

        builder.Entity<OperationRequest>().HasData(operationRequest);

    }

    private void IdentityBootstrap(ModelBuilder builder) {

        string adminRoleId = Guid.NewGuid().ToString();
        string doctorRoleId = Guid.NewGuid().ToString();
        string nurseRoleId = Guid.NewGuid().ToString();
        string technicianRoleId = Guid.NewGuid().ToString();
        string patientRoleId = Guid.NewGuid().ToString();

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole {
                Id = adminRoleId,
                Name = HospitalRoles.Admin,
                NormalizedName = HospitalRoles.Admin.ToUpper()
            }, new IdentityRole {
                Id = doctorRoleId,
                Name = HospitalRoles.Doctor,
                NormalizedName = HospitalRoles.Doctor.ToUpper()
            }, new IdentityRole {
                Id = nurseRoleId,
                Name = HospitalRoles.Nurse,
                NormalizedName = HospitalRoles.Nurse.ToUpper()
            }, new IdentityRole {
                Id = technicianRoleId,
                Name = HospitalRoles.Technician,
                NormalizedName = HospitalRoles.Technician.ToUpper()
            }, new IdentityRole {
                Id = patientRoleId,
                Name = HospitalRoles.Patient,
                NormalizedName = HospitalRoles.Patient.ToUpper()
            }
        );

        SeedHospitalUser(
            builder,
            adminRoleId,
            "admin",
            "admin@hospital.com",
            "adminpassword");

        SeedHospitalUser(builder,
            doctorRoleId,
            "doctor",
            "doctor@hospital.com",
            "doctorpassword");

        SeedHospitalUser(builder,
            nurseRoleId,
            "nurse",
            "nurse@hospital.com",
            "nursepassword");

        SeedHospitalUser(
            builder,
            technicianRoleId,
            "technician",
            "technician@hospital.com",
            "technicianpassword");

        SeedHospitalUser(
            builder,
            patientRoleId,
            "patient",
            "patient@hospital.com",
            "patientpassword");
            
    }

    private void SeedHospitalUser(ModelBuilder builder, string roleId, string username, string email, string password) {

        string userId = Guid.NewGuid().ToString();
        var adminUser = new IdentityUser {
            Id = userId,
            UserName = username,
            NormalizedUserName = username.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var passwordHasher = new PasswordHasher<IdentityUser>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, password);

        builder.Entity<IdentityUser>().HasData(adminUser);

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> {
                RoleId = roleId,
                UserId = userId
            }
        );
    }

}