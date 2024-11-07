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

        var patientD = new Patient(
            new MedicalRecordNumber("202410000004"),
            new DateOnly(1988, 5, 14),
            new MailAddress("patientD@hospital.com"),
            new PhoneNumber("910555334"),
            Gender.Male,
            new FullName("David Oliveira"),
            new List<Allergy>());

        var patientE = new Patient(
            new MedicalRecordNumber("202410000005"),
            new DateOnly(1992, 8, 22),
            new MailAddress("patientE@hospital.com"),
            new PhoneNumber("910555335"),
            Gender.Female,
            new FullName("Emma Sousa"),
            new List<Allergy>());

        var patientF = new Patient(
            new MedicalRecordNumber("202410000006"),
            new DateOnly(1985, 3, 10),
            new MailAddress("patientF@hospital.com"),
            new PhoneNumber("910555336"),
            Gender.Male,
            new FullName("Felipe Costa"),
            new List<Allergy>());

        var patientG = new Patient(
            new MedicalRecordNumber("202410000007"),
            new DateOnly(2000, 11, 2),
            new MailAddress("patientG@hospital.com"),
            new PhoneNumber("910555337"),
            Gender.Female,
            new FullName("Gabriela Santos"),
            new List<Allergy>());

        var patientH = new Patient(
            new MedicalRecordNumber("202410000008"),
            new DateOnly(1990, 7, 19),
            new MailAddress("patientH@hospital.com"),
            new PhoneNumber("910555338"),
            Gender.Male,
            new FullName("Henrique Almeida"),
            new List<Allergy>());

        var patientI = new Patient(
            new MedicalRecordNumber("202410000009"),
            new DateOnly(1994, 1, 15),
            new MailAddress("patientI@hospital.com"),
            new PhoneNumber("910555339"),
            Gender.Female,
            new FullName("Isabel Pereira"),
            new List<Allergy>());

        var patientJ = new Patient(
            new MedicalRecordNumber("202410000010"),
            new DateOnly(1982, 6, 8),
            new MailAddress("patientJ@hospital.com"),
            new PhoneNumber("910555340"),
            Gender.Male,
            new FullName("João Lima"),
            new List<Allergy>());

        var patientK = new Patient(
            new MedicalRecordNumber("202410000011"),
            new DateOnly(1996, 9, 25),
            new MailAddress("patientK@hospital.com"),
            new PhoneNumber("910555341"),
            Gender.Female,
            new FullName("Karina Martins"),
            new List<Allergy>());

        var patientL = new Patient(
            new MedicalRecordNumber("202410000012"),
            new DateOnly(1987, 4, 12),
            new MailAddress("patientL@hospital.com"),
            new PhoneNumber("910555342"),
            Gender.Male,
            new FullName("Lucas Rodrigues"),
            new List<Allergy>());

        var patientM = new Patient(
            new MedicalRecordNumber("202410000013"),
            new DateOnly(1993, 12, 5),
            new MailAddress("patientM@hospital.com"),
            new PhoneNumber("910555343"),
            Gender.Female,
            new FullName("Marta Silva"),
            new List<Allergy>());


        var staffDoctor1 = new Staff(
            HospitalRoles.Doctor,
            "pedro",
            new MailAddress("pedro@hospital.com"),
            new PhoneNumber("910555111"),
            new FullName("Pedro Carvalho Oliveira Monteiro"),
            new LicenseNumber("f47ac10b-08cc-4372-a507-0e02b2d3d479"));

        var staffDoctor2 = new Staff(
            HospitalRoles.Doctor,
            "andre",
            new MailAddress("andre@hospital.com"),
            new PhoneNumber("920555222"),
            new FullName("André de Sousa Ferreira"),
            new LicenseNumber("f47ac10b-58cc-4372-a567-0e02b2c3d479"));


        var staffDoctor3 = new Staff(
            HospitalRoles.Doctor,
            "tiago",
            new MailAddress("tiago@hospital.com"),
            new PhoneNumber("930555333"),
            new FullName("Tiago Filipe Carvalho Nunes"),
            new LicenseNumber("f57ac10b-68cc-5372-a567-1e02b2c3d479"));

        var staffNurse = new Staff(
            HospitalRoles.Nurse,
            "nurse",
            new MailAddress("nurse@hospital.com"),        
            new PhoneNumber("910555567"),                   
            new FullName("Nurse One"),                      
            new LicenseNumber("n47ac10b-58cc-4372-a567-0e02b2c3d481")  
        );


        var operationTypeA = new OperationType(new OperationName("ACL reconstruction"));
        var operationTypeB = new OperationType(new OperationName("Knee replacement"));
        var operationTypeC = new OperationType(new OperationName("Shoulder replacement"));
        var operationTypeD = new OperationType(new OperationName("Hip replacement"));
        var operationTypeE = new OperationType(new OperationName("Meniscal inury treatment"));
        var operationTypeF = new OperationType(new OperationName("Rotator cuff repair"));
        var operationTypeG = new OperationType(new OperationName("Ankle ligaments repair"));
        var operationTypeH = new OperationType(new OperationName("Lumbar disectomy"));
        var operationTypeI = new OperationType(new OperationName("Trigger finger"));
        var operationTypeJ = new OperationType(new OperationName("Carpal tunnel syndrome"));


        modelBuilder.Entity<Patient>().HasData(
            patientA,
            patientB,
            patientC,
            patientD,
            patientE,
            patientF,
            patientG,
            patientH,
            patientI,
            patientJ,
            patientK,
            patientL,
            patientM);

        modelBuilder.Entity<Staff>().HasData(
            staffDoctor1,
            staffDoctor2,
            staffDoctor3,
            staffNurse);

        modelBuilder.Entity<OperationType>().HasData(
            operationTypeA,
            operationTypeB,
            operationTypeC,
            operationTypeD,
            operationTypeE,
            operationTypeF,
            operationTypeG,
            operationTypeH,
            operationTypeI,
            operationTypeJ);

        SeedOperationRequest(modelBuilder,
            staffDoctor1,
            patientA,
            operationTypeA,
            OperationRequestPriority.Elective,
            new DateTime(2024, 12, 1),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor1,
            patientB,
            operationTypeB,
            OperationRequestPriority.Urgent,
            new DateTime(2024, 12, 15),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor1,
            patientC,
            operationTypeC,
            OperationRequestPriority.Emergency,
            new DateTime(2025, 1, 10),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor1,
            patientD,
            operationTypeD,
            OperationRequestPriority.Urgent,
            new DateTime(2025, 2, 5),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor1,
            patientM,
            operationTypeE,
            OperationRequestPriority.Emergency,
            new DateTime(2025, 3, 15),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor1,
            patientF,
            operationTypeG,
            OperationRequestPriority.Elective,
            new DateTime(2025, 4, 1),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor2,
            patientE,
            operationTypeE,
            OperationRequestPriority.Elective,
            new DateTime(2024, 12, 5),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor2,
            patientF,
            operationTypeF,
            OperationRequestPriority.Urgent,
            new DateTime(2025, 1, 20),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor2,
            patientG,
            operationTypeG,
            OperationRequestPriority.Emergency,
            new DateTime(2025, 2, 15),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor2,
            patientH,
            operationTypeH,
            OperationRequestPriority.Urgent,
            new DateTime(2025, 3, 10),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor2,
            patientI,
            operationTypeI,
            OperationRequestPriority.Emergency,
            new DateTime(2025, 4, 10),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor3,
            patientI,
            operationTypeI,
            OperationRequestPriority.Elective,
            new DateTime(2024, 12, 10),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor3,
            patientJ,
            operationTypeJ,
            OperationRequestPriority.Urgent,
            new DateTime(2024, 12, 25),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor3,
            patientK,
            operationTypeA,
            OperationRequestPriority.Emergency,
            new DateTime(2025, 1, 30),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor3,
            patientL,
            operationTypeB,
            OperationRequestPriority.Urgent,
            new DateTime(2025, 2, 20),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor3,
            patientM,
            operationTypeC,
            OperationRequestPriority.Urgent,
            new DateTime(2025, 3, 5),
            OperationRequestStatus.Pending);

        SeedOperationRequest(modelBuilder,
            staffDoctor3,
            patientA,
            operationTypeD,
            OperationRequestPriority.Emergency,
            new DateTime(2025, 4, 20),
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
            doctorRoleId,
            "andre",
            "andre@hospital.com",
            "andrepassword");

        SeedHospitalUser(builder,
            doctorRoleId,
            "tiago",
            "tiago@hospital.com",
            "tiagopassword");

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