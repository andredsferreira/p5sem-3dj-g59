﻿// <auto-generated />
using System;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDDNetCore.Migrations.DDDSample1Db
{
    [DbContext(typeof(DDDSample1DbContext))]
    [Migration("20241027160846_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DDDSample1.Domain.DomainLogs.DomainLog", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("ActionType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("DomainLog", "projeto5sem");
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationRequests.OperationRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("operationTypeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("patientId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("priority")
                        .HasColumnType("longtext");

                    b.Property<int>("requestStatus")
                        .HasColumnType("int");

                    b.Property<string>("staffId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("operationTypeId");

                    b.HasIndex("patientId");

                    b.HasIndex("staffId");

                    b.ToTable("OperationRequest", "projeto5sem");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6cd8cfd6-c680-4fc6-9aa1-200d1b5b58e4"),
                            dateTime = new DateTime(2024, 10, 27, 16, 8, 45, 780, DateTimeKind.Local).AddTicks(1855),
                            operationTypeId = new Guid("20b4ad78-ff87-4084-9d15-5deccb6e3eca"),
                            patientId = "90603747-e600-44a7-aa45-e5289d9a9e53",
                            priority = "none",
                            requestStatus = 0,
                            staffId = "1309f93d-0ad4-4801-b8cb-1f0ac9c1bb2f"
                        },
                        new
                        {
                            Id = new Guid("d4b17c15-838c-4620-89ae-219e141bd72b"),
                            dateTime = new DateTime(2024, 10, 27, 16, 8, 45, 780, DateTimeKind.Local).AddTicks(1964),
                            operationTypeId = new Guid("e6b916df-aa8d-4d39-9261-f78585e0b23a"),
                            patientId = "ca33686c-bff7-4650-848d-2d14b54d347b",
                            priority = "top",
                            requestStatus = 0,
                            staffId = "1309f93d-0ad4-4801-b8cb-1f0ac9c1bb2f"
                        });
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationTypes.OperationType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("MinAnesthetist")
                        .HasColumnType("int");

                    b.Property<int>("MinCirculatingNurse")
                        .HasColumnType("int");

                    b.Property<int>("MinDoctor")
                        .HasColumnType("int");

                    b.Property<int>("MinInstrumentingNurse")
                        .HasColumnType("int");

                    b.Property<int>("MinMedicalActionAssistant")
                        .HasColumnType("int");

                    b.Property<int>("MinNurseAnaesthetist")
                        .HasColumnType("int");

                    b.Property<int>("MinXRayTechnician")
                        .HasColumnType("int");

                    b.Property<int>("Specialization")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("anaesthesiaTime")
                        .HasColumnType("int");

                    b.Property<int?>("cleaningTime")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("surgeryTime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("OperationType", "projeto5sem");

                    b.HasData(
                        new
                        {
                            Id = new Guid("20b4ad78-ff87-4084-9d15-5deccb6e3eca"),
                            MinAnesthetist = 1,
                            MinCirculatingNurse = 1,
                            MinDoctor = 1,
                            MinInstrumentingNurse = 1,
                            MinMedicalActionAssistant = 1,
                            MinNurseAnaesthetist = 1,
                            MinXRayTechnician = 1,
                            Specialization = 0,
                            Status = "ACTIVE",
                            anaesthesiaTime = 0,
                            cleaningTime = 0,
                            name = "ACL Reconstruction",
                            surgeryTime = 0
                        },
                        new
                        {
                            Id = new Guid("e6b916df-aa8d-4d39-9261-f78585e0b23a"),
                            MinAnesthetist = 1,
                            MinCirculatingNurse = 1,
                            MinDoctor = 1,
                            MinInstrumentingNurse = 1,
                            MinMedicalActionAssistant = 1,
                            MinNurseAnaesthetist = 1,
                            MinXRayTechnician = 1,
                            Specialization = 0,
                            Status = "ACTIVE",
                            anaesthesiaTime = 0,
                            cleaningTime = 0,
                            name = "Knee Replacement",
                            surgeryTime = 0
                        },
                        new
                        {
                            Id = new Guid("959202e8-4b66-4338-8631-e10cc2ee3e11"),
                            MinAnesthetist = 1,
                            MinCirculatingNurse = 1,
                            MinDoctor = 1,
                            MinInstrumentingNurse = 1,
                            MinMedicalActionAssistant = 1,
                            MinNurseAnaesthetist = 1,
                            MinXRayTechnician = 1,
                            Specialization = 0,
                            Status = "ACTIVE",
                            anaesthesiaTime = 0,
                            cleaningTime = 0,
                            name = "Shoulder Replacement",
                            surgeryTime = 0
                        });
                });

            modelBuilder.Entity("DDDSample1.Domain.Patients.Allergy", b =>
                {
                    b.Property<Guid>("allergyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("PatientId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("allergyName")
                        .HasColumnType("longtext");

                    b.HasKey("allergyId");

                    b.HasIndex("PatientId");

                    b.ToTable("Allergy");
                });

            modelBuilder.Entity("DDDSample1.Domain.Patients.Patient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MedicalRecordNumber")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("MedicalRecordNumber")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("Patient", "projeto5sem");

                    b.HasData(
                        new
                        {
                            Id = "90603747-e600-44a7-aa45-e5289d9a9e53",
                            DateOfBirth = new DateOnly(2001, 10, 21),
                            Email = "patientA@hospital.com",
                            FullName = "João Camião",
                            Gender = "Male",
                            MedicalRecordNumber = "202410000001",
                            PhoneNumber = "910555111"
                        },
                        new
                        {
                            Id = "ca33686c-bff7-4650-848d-2d14b54d347b",
                            DateOfBirth = new DateOnly(1998, 5, 14),
                            Email = "patientB@hospital.com",
                            FullName = "Bruno Silva",
                            Gender = "Male",
                            MedicalRecordNumber = "202410000002",
                            PhoneNumber = "910555222"
                        },
                        new
                        {
                            Id = "5d40fa3a-ec51-49b1-922f-dd0d5c6c6d7f",
                            DateOfBirth = new DateOnly(1995, 12, 30),
                            Email = "patientC@hospital.com",
                            FullName = "Carla Ferreira",
                            Gender = "Female",
                            MedicalRecordNumber = "202410000003",
                            PhoneNumber = "910555333"
                        });
                });

            modelBuilder.Entity("DDDSample1.Domain.Staffs.Staff", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<string>("IdentityUsername")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LicenseNumber")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("StaffRole")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LicenseNumber")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Staff", "projeto5sem");

                    b.HasData(
                        new
                        {
                            Id = "1309f93d-0ad4-4801-b8cb-1f0ac9c1bb2f",
                            IdentityUsername = "doctor",
                            StaffRole = "Doctor"
                        },
                        new
                        {
                            Id = "34f1894e-cfd3-408d-a6c4-461e5d4316b4",
                            Email = "doctor2@hospital.com",
                            FullName = "Doctor 2",
                            IdentityUsername = "doctor2",
                            LicenseNumber = "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                            PhoneNumber = "910555444",
                            StaffRole = "Doctor"
                        },
                        new
                        {
                            Id = "27b0d99e-f9b1-417a-8a5b-8b636e4710d2",
                            IdentityUsername = "nurese",
                            StaffRole = "Nurse"
                        });
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationRequests.OperationRequest", b =>
                {
                    b.HasOne("DDDSample1.Domain.OperationTypes.OperationType", "operationType")
                        .WithMany("OperationRequests")
                        .HasForeignKey("operationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDDSample1.Domain.Patients.Patient", "patient")
                        .WithMany("OperationRequests")
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DDDSample1.Domain.Staffs.Staff", "staff")
                        .WithMany("OperationRequests")
                        .HasForeignKey("staffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("operationType");

                    b.Navigation("patient");

                    b.Navigation("staff");
                });

            modelBuilder.Entity("DDDSample1.Domain.Patients.Allergy", b =>
                {
                    b.HasOne("DDDSample1.Domain.Patients.Patient", null)
                        .WithMany("Allergies")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DDDSample1.Domain.OperationTypes.OperationType", b =>
                {
                    b.Navigation("OperationRequests");
                });

            modelBuilder.Entity("DDDSample1.Domain.Patients.Patient", b =>
                {
                    b.Navigation("Allergies");

                    b.Navigation("OperationRequests");
                });

            modelBuilder.Entity("DDDSample1.Domain.Staffs.Staff", b =>
                {
                    b.Navigation("OperationRequests");
                });
#pragma warning restore 612, 618
        }
    }
}