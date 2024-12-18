using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "projeto5sem");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DomainLog",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ObjectType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActionType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Message = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainLog", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patient",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedicalRecordNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Specialization",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    codeSpec = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    designation = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StaffRole = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdentityUsername = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LicenseNumber = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SurgeryRoom",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoomStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AssignedEquipment = table.Column<string>(type: "JSON", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaintenanceSlots = table.Column<string>(type: "JSON", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryRoom", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    allergyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    allergyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PatientId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.allergyId);
                    table.ForeignKey(
                        name: "FK_Allergy_Patient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "projeto5sem",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OperationType",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anaesthesiaTime = table.Column<int>(type: "int", nullable: true),
                    surgeryTime = table.Column<int>(type: "int", nullable: true),
                    cleaningTime = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpecializationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    MinDoctor = table.Column<int>(type: "int", nullable: false),
                    MinAnesthetist = table.Column<int>(type: "int", nullable: false),
                    MinInstrumentingNurse = table.Column<int>(type: "int", nullable: false),
                    MinCirculatingNurse = table.Column<int>(type: "int", nullable: false),
                    MinNurseAnaesthetist = table.Column<int>(type: "int", nullable: false),
                    MinXRayTechnician = table.Column<int>(type: "int", nullable: false),
                    MinMedicalActionAssistant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationType_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalSchema: "projeto5sem",
                        principalTable: "Specialization",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OperationRequest",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    staffId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    patientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    operationTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    priority = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requestStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationRequest_OperationType_operationTypeId",
                        column: x => x.operationTypeId,
                        principalSchema: "projeto5sem",
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationRequest_Patient_patientId",
                        column: x => x.patientId,
                        principalSchema: "projeto5sem",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OperationRequest_Staff_staffId",
                        column: x => x.staffId,
                        principalSchema: "projeto5sem",
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointment",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    OperationRequestId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SurgeryRoomId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppointmentStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_OperationRequest_OperationRequestId",
                        column: x => x.OperationRequestId,
                        principalSchema: "projeto5sem",
                        principalTable: "OperationRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_SurgeryRoom_SurgeryRoomId",
                        column: x => x.SurgeryRoomId,
                        principalSchema: "projeto5sem",
                        principalTable: "SurgeryRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "500e872f-eb2a-4a43-8b2c-8afdbec21d7a", null, "Nurse", "NURSE" },
                    { "729aaa37-7a19-4600-9d72-7f98a52e2f73", null, "Technician", "TECHNICIAN" },
                    { "cbaffa3c-49cb-4c94-8318-65267bc23109", null, "Patient", "PATIENT" },
                    { "d85980f4-43b6-428f-af9c-e423ea6555ac", null, "Admin", "ADMIN" },
                    { "fd3f89e0-c046-45a1-967d-5b330a5a5ab0", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "33f583c9-6b7a-40d6-a5d5-b7c41cbb1ef8", 0, "384a3dac-1b40-4f9a-ad9e-3587b376948f", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEAKRayhb10s14x9Ynf6H2sdBYObDFKrVNXlmzq2gONkW5xYJRnlCtajNnkgWih9b8Q==", null, false, "0fbde82a-7f9e-41da-9693-97fc4b2f6124", false, "technician" },
                    { "49b4b520-025b-4e6e-8406-3ac69054b0cd", 0, "16393a12-d26d-44a0-afd6-30f57eb565f2", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEKxXGot2FUtESUhN6jdmX3nr6X1iwHiWbYspP1+e3kJg3FPQamYZE4NrLHvN3IZQUg==", null, false, "63bdd95a-d444-4246-b36c-376a1f56c903", false, "nurse" },
                    { "56a39e87-da40-4af1-acbf-ea63e0ac9c5a", 0, "ffe63d9b-55c2-407e-8786-89286c36a877", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEGJkFmuIENrkNCd8NIRnEyKVFmsdeAU62Ag6dmdHPHxqpBRKTNK6BHTsnCZ6FhIcTQ==", null, false, "63b2596a-3f53-41ad-9a4c-292ce77939c6", false, "doctor" },
                    { "5fea4ba2-988e-4270-9dfb-47908a3a838f", 0, "9a54721c-2afe-4223-9e63-309b59b9e7c9", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEL+e/uCda+O4Me5sRDIfIrCgc9pZrIJHsjPGoIkdgC+2MyqYg1pXO7KC4S9UjzdCMQ==", null, false, "3eb080dc-3f9b-4b98-8d81-5f1ea3e71261", false, "patient" },
                    { "978a18d2-dcca-4006-95ba-6c242028b9d4", 0, "1bf5b886-561e-4a1b-9a92-380fd9179c22", "andre@hospital.com", true, false, null, "ANDRE@HOSPITAL.COM", "ANDRE", "AQAAAAIAAYagAAAAEBKcMz3P3phEy6+hJ2acSFpI/+CIWjn77/aH3uGnpIYTSaPzvEpejBNR3K1+Lf7GiQ==", null, false, "1c3b4806-4195-4c47-a835-cc3df3720056", false, "andre" },
                    { "e7e57f6f-dfca-4460-8ee3-dd286563f68a", 0, "de41460f-e2f6-4542-9b74-cac90a0a9c11", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAECLlNRZ3XSNMRY8Pbz/691Qv4qRSbEsXjbg+K/dYakn8kuVGz1XSk2JPGNwYVOfYig==", null, false, "d20fb56f-51ed-4212-a898-21d10dbb02f3", false, "admin" },
                    { "f8fa680e-0f58-4cde-8ac7-796d200197c6", 0, "a001e182-7638-4876-97b2-f558dde1fa59", "tiago@hospital.com", true, false, null, "TIAGO@HOSPITAL.COM", "TIAGO", "AQAAAAIAAYagAAAAEAa5ZeIQqSlj9c+OHYICfj0IYZlN37TSrP4kq+wjRpy0MT4XINmVuiWk2Lf/KIYBcw==", null, false, "76d486d9-70f3-4353-8275-0946000f52e1", false, "tiago" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "SpecializationId", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("09e86063-d8ea-4821-b4cf-430d3a1c5c3c"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 20, 15, "Lumbar disectomy", 45 },
                    { new Guid("169f3d9b-923a-4852-824b-24e828cc0594"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Hip replacement", 75 },
                    { new Guid("1859092d-58a5-4be8-90fc-2a367ab8e1eb"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Trigger finger", 10 },
                    { new Guid("1fb72761-579f-4af9-87b5-37f7115b8280"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "Rotator cuff repair", 80 },
                    { new Guid("3b515d9f-ad76-4bd0-8e4e-b264ddbe8631"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Carpal tunnel syndrome", 10 },
                    { new Guid("b87ff508-e3a2-42e3-9dca-939393cb39d7"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Shoulder replacement", 90 },
                    { new Guid("bf338c12-bdce-4ff7-a349-9ca6c32f8a28"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "ACL reconstruction", 60 },
                    { new Guid("c9b2f3fb-c5ed-49d1-8824-982a8bab2d21"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 20, "Meniscal inury treatment", 45 },
                    { new Guid("cac81324-91c2-4920-b930-fb934f97a637"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 30, 20, "Ankle ligaments repair", 45 },
                    { new Guid("d7035d06-46b8-4c33-8c1a-b79c1362f127"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Knee replacement", 60 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "0dffb78a-0d61-400f-acf8-9c8404834d1e", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", "patientK@hospital.com" },
                    { "1610319e-ba58-4191-b041-d73c7af03128", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", "patientB@hospital.com" },
                    { "2f64617b-38e3-46d3-83b6-d4a28e90f6fc", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", "patientJ@hospital.com" },
                    { "341730b5-98a9-4292-a9c8-2d36e4fcf281", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", "patientE@hospital.com" },
                    { "3e5b384e-361d-4212-a5d6-6a9d9eec505c", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", "patientI@hospital.com" },
                    { "46854854-0140-4e05-8732-9567c613d6c9", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", "patientC@hospital.com" },
                    { "4a9a767e-68b9-4a04-8687-96b62d6fe855", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", "patientG@hospital.com" },
                    { "54e8888f-0b4e-4e88-951e-74f1b24743e2", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", "patientL@hospital.com" },
                    { "6e3fe5db-0fcd-4690-9523-35e5b2e45c55", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", "patientA@hospital.com" },
                    { "b01d0ec2-c30b-463d-b533-b3ecce66ae22", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", "patientD@hospital.com" },
                    { "c3072c2d-5bf7-4812-8c13-49628faeab55", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", "patientF@hospital.com" },
                    { "cd047d2d-b1e9-42bb-905a-de0f1b85b608", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", "patientH@hospital.com" },
                    { "fdee3798-b3e4-4422-a84e-8775c8020f2d", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", "patientM@hospital.com" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Specialization",
                columns: new[] { "Id", "codeSpec", "description", "designation" },
                values: new object[,]
                {
                    { new Guid("86827cff-5d15-4a38-bd99-36acb888580e"), "PRO", "Prosthetics", "Prosthethiscs" },
                    { new Guid("cac82333-7379-4bc5-9efb-03a7387a1d30"), "ART", "Arthroscopy", "Arthroscopy" },
                    { new Guid("dc97edfd-229d-4632-a053-da2853f7eeae"), "SPN", "Spine", "Spine" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "40eb7cf5-f5f5-4fda-b324-7a8790b9f2c0", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "Nurse" },
                    { "94f0a26f-293d-4d47-a798-b43209ae7425", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "Doctor" },
                    { "bb115771-b20b-45ee-b3c0-05718803bd3c", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "Doctor" },
                    { "cabfe637-4f71-4a76-a8f1-0065fe1919a3", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "Doctor" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "SurgeryRoom",
                columns: new[] { "Id", "AssignedEquipment", "Capacity", "MaintenanceSlots", "Number", "RoomStatus", "RoomType" },
                values: new object[,]
                {
                    { "791f487e-a291-4200-b90e-41d8044afeb7", "[\"Scalpel\",\"Monitor\",\"Table\"]", 10, "[\"28/10/2024=[12:30,13:00];\"]", 201, "Available", "OperatingRoom" },
                    { "82ce05ce-56ab-4d26-805d-bd5af5eb3763", "[\"Scalpel\",\"Monitor\"]", 10, "[\"28/10/2024=[09:30,10:00];\"]", 200, "Available", "OperatingRoom" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "729aaa37-7a19-4600-9d72-7f98a52e2f73", "33f583c9-6b7a-40d6-a5d5-b7c41cbb1ef8" },
                    { "500e872f-eb2a-4a43-8b2c-8afdbec21d7a", "49b4b520-025b-4e6e-8406-3ac69054b0cd" },
                    { "fd3f89e0-c046-45a1-967d-5b330a5a5ab0", "56a39e87-da40-4af1-acbf-ea63e0ac9c5a" },
                    { "cbaffa3c-49cb-4c94-8318-65267bc23109", "5fea4ba2-988e-4270-9dfb-47908a3a838f" },
                    { "fd3f89e0-c046-45a1-967d-5b330a5a5ab0", "978a18d2-dcca-4006-95ba-6c242028b9d4" },
                    { "d85980f4-43b6-428f-af9c-e423ea6555ac", "e7e57f6f-dfca-4460-8ee3-dd286563f68a" },
                    { "fd3f89e0-c046-45a1-967d-5b330a5a5ab0", "f8fa680e-0f58-4cde-8ac7-796d200197c6" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("0a4cf163-1e20-4294-9276-3d0d8265d195"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9b2f3fb-c5ed-49d1-8824-982a8bab2d21"), "fdee3798-b3e4-4422-a84e-8775c8020f2d", "Emergency", "Pending", "94f0a26f-293d-4d47-a798-b43209ae7425" },
                    { new Guid("142f9bb0-f7db-4784-b819-54e2cc197e34"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1fb72761-579f-4af9-87b5-37f7115b8280"), "c3072c2d-5bf7-4812-8c13-49628faeab55", "Urgent", "Pending", "bb115771-b20b-45ee-b3c0-05718803bd3c" },
                    { new Guid("2da4eed8-6f8b-49b2-bf6f-15c383a1994a"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c9b2f3fb-c5ed-49d1-8824-982a8bab2d21"), "341730b5-98a9-4292-a9c8-2d36e4fcf281", "Elective", "Pending", "bb115771-b20b-45ee-b3c0-05718803bd3c" },
                    { new Guid("320eb07c-0865-43b0-b28e-4ce609177139"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bf338c12-bdce-4ff7-a349-9ca6c32f8a28"), "6e3fe5db-0fcd-4690-9523-35e5b2e45c55", "Elective", "Pending", "94f0a26f-293d-4d47-a798-b43209ae7425" },
                    { new Guid("60d8f43d-c793-4a9d-96b0-26a1f5064da1"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d7035d06-46b8-4c33-8c1a-b79c1362f127"), "1610319e-ba58-4191-b041-d73c7af03128", "Urgent", "Pending", "94f0a26f-293d-4d47-a798-b43209ae7425" },
                    { new Guid("7c64540a-0208-419d-80b2-73b3a9543f13"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bf338c12-bdce-4ff7-a349-9ca6c32f8a28"), "0dffb78a-0d61-400f-acf8-9c8404834d1e", "Emergency", "Pending", "cabfe637-4f71-4a76-a8f1-0065fe1919a3" },
                    { new Guid("961ffc01-1d47-44ae-895c-d84b9e386d24"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cac81324-91c2-4920-b930-fb934f97a637"), "4a9a767e-68b9-4a04-8687-96b62d6fe855", "Emergency", "Pending", "bb115771-b20b-45ee-b3c0-05718803bd3c" },
                    { new Guid("9daedf05-8280-4e6f-9a84-74a980a6b6df"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("169f3d9b-923a-4852-824b-24e828cc0594"), "6e3fe5db-0fcd-4690-9523-35e5b2e45c55", "Emergency", "Pending", "cabfe637-4f71-4a76-a8f1-0065fe1919a3" },
                    { new Guid("b3d1aa0b-69c7-4efc-a046-6b856ba00e5b"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b87ff508-e3a2-42e3-9dca-939393cb39d7"), "46854854-0140-4e05-8732-9567c613d6c9", "Emergency", "Pending", "94f0a26f-293d-4d47-a798-b43209ae7425" },
                    { new Guid("b4d24e85-d59d-487c-a99d-5067e638cb94"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3b515d9f-ad76-4bd0-8e4e-b264ddbe8631"), "2f64617b-38e3-46d3-83b6-d4a28e90f6fc", "Urgent", "Pending", "cabfe637-4f71-4a76-a8f1-0065fe1919a3" },
                    { new Guid("b805f725-e817-4fb7-a691-11467eb500e2"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1859092d-58a5-4be8-90fc-2a367ab8e1eb"), "3e5b384e-361d-4212-a5d6-6a9d9eec505c", "Elective", "Pending", "cabfe637-4f71-4a76-a8f1-0065fe1919a3" },
                    { new Guid("d3ca1b20-06be-4e65-9ee0-614cff9e23ae"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1859092d-58a5-4be8-90fc-2a367ab8e1eb"), "3e5b384e-361d-4212-a5d6-6a9d9eec505c", "Emergency", "Pending", "bb115771-b20b-45ee-b3c0-05718803bd3c" },
                    { new Guid("df860ac9-7e05-4b03-a158-45a0766b0b98"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cac81324-91c2-4920-b930-fb934f97a637"), "c3072c2d-5bf7-4812-8c13-49628faeab55", "Elective", "Pending", "94f0a26f-293d-4d47-a798-b43209ae7425" },
                    { new Guid("e11f5835-deaa-4a33-bab4-aaa588e15eb8"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("09e86063-d8ea-4821-b4cf-430d3a1c5c3c"), "cd047d2d-b1e9-42bb-905a-de0f1b85b608", "Urgent", "Pending", "bb115771-b20b-45ee-b3c0-05718803bd3c" },
                    { new Guid("e2f1ff93-9487-4d36-a4d9-197594b0a77e"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b87ff508-e3a2-42e3-9dca-939393cb39d7"), "fdee3798-b3e4-4422-a84e-8775c8020f2d", "Urgent", "Pending", "cabfe637-4f71-4a76-a8f1-0065fe1919a3" },
                    { new Guid("ef5e68ae-2def-4ff7-a3ad-bbee6c57ed82"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("169f3d9b-923a-4852-824b-24e828cc0594"), "b01d0ec2-c30b-463d-b533-b3ecce66ae22", "Urgent", "Pending", "94f0a26f-293d-4d47-a798-b43209ae7425" },
                    { new Guid("f3a302d2-92fc-4723-9471-6387cfe28617"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d7035d06-46b8-4c33-8c1a-b79c1362f127"), "54e8888f-0b4e-4e88-951e-74f1b24743e2", "Urgent", "Pending", "cabfe637-4f71-4a76-a8f1-0065fe1919a3" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Appointment",
                columns: new[] { "Id", "AppointmentStatus", "DateTime", "OperationRequestId", "SurgeryRoomId" },
                values: new object[,]
                {
                    { new Guid("02185caa-870b-46b4-a422-ed44f46182cd"), "Scheduled", "28/10/2024 18:30:00", new Guid("60d8f43d-c793-4a9d-96b0-26a1f5064da1"), "791f487e-a291-4200-b90e-41d8044afeb7" },
                    { new Guid("abe62c1d-5006-4b81-9e7a-bd976fe0d7ae"), "Scheduled", "28/10/2024 10:30:00", new Guid("320eb07c-0865-43b0-b28e-4ce609177139"), "82ce05ce-56ab-4d26-805d-bd5af5eb3763" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_PatientId",
                table: "Allergy",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_OperationRequestId",
                schema: "projeto5sem",
                table: "Appointment",
                column: "OperationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_SurgeryRoomId",
                schema: "projeto5sem",
                table: "Appointment",
                column: "SurgeryRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationRequest_operationTypeId",
                schema: "projeto5sem",
                table: "OperationRequest",
                column: "operationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationRequest_patientId",
                schema: "projeto5sem",
                table: "OperationRequest",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationRequest_staffId",
                schema: "projeto5sem",
                table: "OperationRequest",
                column: "staffId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationType_name",
                schema: "projeto5sem",
                table: "OperationType",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationType_SpecializationId",
                schema: "projeto5sem",
                table: "OperationType",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Email",
                schema: "projeto5sem",
                table: "Patient",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_MedicalRecordNumber",
                schema: "projeto5sem",
                table: "Patient",
                column: "MedicalRecordNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_PhoneNumber",
                schema: "projeto5sem",
                table: "Patient",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserEmail",
                schema: "projeto5sem",
                table: "Patient",
                column: "UserEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialization_codeSpec",
                schema: "projeto5sem",
                table: "Specialization",
                column: "codeSpec",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialization_designation",
                schema: "projeto5sem",
                table: "Specialization",
                column: "designation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Email",
                schema: "projeto5sem",
                table: "Staff",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_LicenseNumber",
                schema: "projeto5sem",
                table: "Staff",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_PhoneNumber",
                schema: "projeto5sem",
                table: "Staff",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryRoom_Number",
                schema: "projeto5sem",
                table: "SurgeryRoom",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "Appointment",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DomainLog",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "OperationRequest",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "SurgeryRoom",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OperationType",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "Patient",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "Specialization",
                schema: "projeto5sem");
        }
    }
}
