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
using DDDSample1.Domain.DomainLogs;
using DDDSample1.Infrastructure.DomainLogs;
using System.Net.Mail;

namespace DDDSample1.Infrastructure;

public class DDDSample1DbContext : DbContext {

    public virtual DbSet<OperationRequest> OperationRequests { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<OperationType> OperationTypes { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<DomainLog> DomainLogs { get; set; }

    public DDDSample1DbContext(DbContextOptions<DDDSample1DbContext> options) : base(options) {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new OperationRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DomainLogEntityTypeConfiguration());

        var patientA = new Patient(new MedicalRecordNumber("202410000001"), new DateOnly(2001, 10, 21), new MailAddress("patientA@hospital.com"), new PhoneNumber("910555111"), Gender.Male, new FullName("João Camião"), new List<Allergy>());
        var patientB = new Patient(new MedicalRecordNumber("202410000002"), new DateOnly(1998, 5, 14), new MailAddress("patientB@hospital.com"), new PhoneNumber("910555222"), Gender.Male, new FullName("Bruno Silva"), new List<Allergy>());
        var patientC = new Patient(new MedicalRecordNumber("202410000003"), new DateOnly(1995, 12, 30), new MailAddress("patientC@hospital.com"), new PhoneNumber("910555333"), Gender.Female, new FullName("Carla Ferreira"), new List<Allergy>());

        var staffDoctor = new Staff(HospitalRoles.Doctor, "doctor");
        var staffNurse = new Staff(HospitalRoles.Nurse, "nurese");
        var staffDoctor2 = new Staff(HospitalRoles.Doctor, "doctor2", new MailAddress("doctor2@hospital.com"), new PhoneNumber("910555444"), new FullName("Doctor 2"), new LicenseNumber("f47ac10b-58cc-4372-a567-0e02b2c3d479"));

        var operationTypeA = new OperationType(new OperationName("ACL Reconstruction"));
        var operationTypeB = new OperationType(new OperationName("Knee Replacement"));
        var operationTypeC = new OperationType(new OperationName("Shoulder Replacement"));

        modelBuilder.Entity<Patient>().HasData(patientA, patientB, patientC);

        modelBuilder.Entity<Staff>().HasData(staffDoctor,staffDoctor2, staffNurse);


        modelBuilder.Entity<OperationType>().HasData(operationTypeA, operationTypeB, operationTypeC);

        SeedOperationRequest(modelBuilder, patientA, staffDoctor, operationTypeA, "none", DateTime.Now, "Pending");

        SeedOperationRequest(modelBuilder, patientB, staffDoctor, operationTypeB, "top", DateTime.Now, "Pending.");


        base.OnModelCreating(modelBuilder);
    }

    private void SeedOperationRequest(ModelBuilder builder, Patient patient, Staff doctor, OperationType operationType, string priority, DateTime dateTime, string requestStatus) {
        var operationRequest = new OperationRequest(patient.Id, doctor.Id, operationType.Id, priority, dateTime, requestStatus);

        builder.Entity<OperationRequest>().HasData(operationRequest);

    }

    private void SeedPatient(ModelBuilder builder, MedicalRecordNumber medicalRecordNumber, DateOnly dateOfBirth, MailAddress email, PhoneNumber phoneNumber, Gender gender, FullName fullName, List<string> allergies) {
        List<Allergy> allergiesAllergy = allergies
            .Select(allergyName => new Allergy(allergyName))
            .ToList();
        var patient = new Patient(medicalRecordNumber, dateOfBirth, email, phoneNumber, gender, fullName, allergiesAllergy);
        var log = new DomainLog(LogObjectType.Patient, LogActionType.Creation, string.Format("Created a new Patient (Medical Record Number = {0}, Name = {1}, Email = {2}, PhoneNumber = {3})",
                        patient.MedicalRecordNumber.Record, patient.FullName.Full, patient.Email, patient.PhoneNumber));
        builder.Entity<Patient>().HasData(patient);
        builder.Entity<DomainLog>().HasData(log);
    }

    private void SeedStaff(ModelBuilder builder, string role, string identityUsername, MailAddress email, PhoneNumber phoneNumber, FullName fullName, LicenseNumber licenseNumber) {
        var staff = new Staff(role, identityUsername, email, phoneNumber, fullName, licenseNumber);
        var log = new DomainLog(LogObjectType.Staff, LogActionType.Creation, string.Format("Created a new Staff (License Number = {0}, Name = {1}, Email = {2}, PhoneNumber = {3})",
                        staff.LicenseNumber, staff.FullName.Full, staff.Email, staff.PhoneNumber));
        builder.Entity<Staff>().HasData(staff);
        builder.Entity<DomainLog>().HasData(log);
    }

    private void SeedStaff(ModelBuilder builder,Staff staff){
        var log = new DomainLog(LogObjectType.Staff, LogActionType.Creation, string.Format("Created a new Staff (License Number = {0}, Name = {1}, Email = {2}, PhoneNumber = {3})",
                        staff.LicenseNumber, staff.FullName.Full, staff.Email, staff.PhoneNumber));
        builder.Entity<Staff>().HasData(staff);
        builder.Entity<DomainLog>().HasData(log);
    }


}