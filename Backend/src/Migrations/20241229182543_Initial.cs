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
                    { "34b9fa00-a5ce-416a-8686-ad4a93f78f16", null, "Patient", "PATIENT" },
                    { "638e2377-9757-472c-a498-1038e80b6ec1", null, "Admin", "ADMIN" },
                    { "64c8e920-3960-46fa-8e8c-8b20203c59aa", null, "Technician", "TECHNICIAN" },
                    { "9b4c17cd-5d67-4a04-b66b-4c3d283309e1", null, "Nurse", "NURSE" },
                    { "d31d8ad6-2a97-4c27-8b1c-106c393cd810", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "31771157-4697-4766-9f35-aa09c696c793", 0, "e73a58c7-58f7-44bc-9d38-ed098e75b84b", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAECawDOkoQV86fXvS//Se8dPegErJ2bwg1QeeaQOSHY2MJi4U1sla4ZkTmhEPCxGvkA==", null, false, "a9bf14a4-0e7b-4e88-b3c8-aebfcacbc1b7", false, "doctor" },
                    { "a4e34928-0b86-4c39-9087-42f92865c19f", 0, "b2191e9d-3d18-40ca-a80b-d656746f2f85", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEAqdyVpL7SjE8z9aHeZaUnA/ZEoaH1fwBEghUOxvsFg2zPL5Lq66DJOLMrWZn0WlPg==", null, false, "5c192409-de2b-4d19-82e3-38eeb3aa5f44", false, "nurse" },
                    { "b0974e73-c88a-4c10-b2cb-2f96a45f27f9", 0, "bbad80ea-97d9-4947-9e4b-99ef4b2da4f3", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAENcXz8eKmfNWbo9V914TKUCxWexIIZ5/gGYzR9UTh7uBGqZyXuHcsTJ//SxcqnEIXA==", null, false, "e8db0898-9af6-4eda-86d1-b914e5882c05", false, "technician" },
                    { "cf6e441b-d007-4655-bcbc-ccbdc62a8852", 0, "b1edfb4c-e1d8-4e8c-82d9-a6164ab76afe", "tiago@hospital.com", true, false, null, "TIAGO@HOSPITAL.COM", "TIAGO", "AQAAAAIAAYagAAAAENJ8lVPVPOX5CHq3zwMGpkJRyQOWzDfPfPGTffpsop3USGQv0dpMmAHPra0+VzbCZA==", null, false, "05351278-7192-4578-9dfc-38c7e8e18959", false, "tiago" },
                    { "da187419-b6c3-4be4-9221-c9123337ca99", 0, "ca9c820b-5a52-4f0a-990b-1fca597e11dd", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEL5mhZUBoRa057n7F65O9pnnes8cFWEJnfzGlM82DIaU27fr/PalPG36otWSZJwOmg==", null, false, "9b54ee2b-207c-497f-8ee6-2f345d80bd6c", false, "admin" },
                    { "eb64b6ff-2213-4a40-898a-7bdcf964859e", 0, "802b6aa6-6b5a-4a75-8991-14e5d33003fc", "andre@hospital.com", true, false, null, "ANDRE@HOSPITAL.COM", "ANDRE", "AQAAAAIAAYagAAAAEFPtt1lIzztEaEBmX3K8svT3NyjBCcfrrOFv0I/mjp3tjDxjhKxeD5oa1w0R8fyJYg==", null, false, "de224251-93b0-47bb-9d15-6328591e2047", false, "andre" },
                    { "f3dbe6eb-aa9a-455d-b2b0-01b953a7a518", 0, "3f8ea0bb-f61d-4c96-b7cd-6195a5b2a94f", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEAEZcmTNGQce15YxLPb54e7Yw2v2uBWbuhLR3xJck4lgvIO9YIEMw9EkbRTF2NmkYw==", null, false, "94458972-9dac-4b4b-82db-14d831258a7c", false, "patient" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "SpecializationId", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("0529c64a-3ccd-4d59-8bb5-563c07064ec3"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "ACL reconstruction", 60 },
                    { new Guid("0e359c70-6cf5-449c-9e73-c8745f867beb"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Trigger finger", 10 },
                    { new Guid("0ec1ad77-7bde-4afb-a83c-498ba89ec9b8"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 20, "Meniscal inury treatment", 45 },
                    { new Guid("1266af33-ed44-4419-8879-652e2a0ff073"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 30, 20, "Ankle ligaments repair", 45 },
                    { new Guid("65b662ff-eb3a-410f-be57-c6ddf1476de2"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "Rotator cuff repair", 80 },
                    { new Guid("8beac4ef-1a57-40df-8d42-200c155c3606"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Knee replacement", 60 },
                    { new Guid("bad19991-3e1c-4424-bd46-b86d0e04a140"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Hip replacement", 75 },
                    { new Guid("c6a49f17-8071-4db5-9d6a-deecfed61604"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Carpal tunnel syndrome", 10 },
                    { new Guid("f6b16357-d72d-4e11-b48b-c6bfaac52fd8"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Shoulder replacement", 90 },
                    { new Guid("fe672ae2-2da1-4fbb-9d9c-d9cb80f230d8"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 20, 15, "Lumbar disectomy", 45 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "1405015e-7294-49a3-bb5a-c8a8f09ecf06", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", "patientM@hospital.com" },
                    { "474193fe-6b54-4d9d-b4d2-db3bda005660", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", "patientA@hospital.com" },
                    { "50dfebcf-3578-48d1-b868-5427d851bb72", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", "patientC@hospital.com" },
                    { "5f94b8f7-1a1b-4451-82ff-a1c9bc3d4b51", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", "patientB@hospital.com" },
                    { "728f61fb-132e-408d-b1bd-bc7aa6fad1fd", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", "patientF@hospital.com" },
                    { "7c72d74c-f3dd-4c29-a332-1988018074fb", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", "patientK@hospital.com" },
                    { "7fe6ebee-1d7e-45d1-8053-c77635b3c273", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", "patientL@hospital.com" },
                    { "88acd096-79e8-415c-9ce9-0757c2597a84", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", "patientE@hospital.com" },
                    { "9405519c-d200-404e-af58-82da32c9f249", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", "patientJ@hospital.com" },
                    { "9fc45861-7910-44d5-93c0-7893f4659e9b", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", "patientI@hospital.com" },
                    { "a2fabed7-82ce-4e68-b207-fe91f9d33ec8", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", "patientG@hospital.com" },
                    { "ba953560-2def-4f6e-b87a-60f8fac08a79", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", "patientH@hospital.com" },
                    { "d8b6071f-580b-4cf3-aa59-803ca83f6dfe", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", "patientD@hospital.com" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Specialization",
                columns: new[] { "Id", "codeSpec", "description", "designation" },
                values: new object[,]
                {
                    { new Guid("639634a6-12e1-486e-b107-29dd6839ef9f"), "PRO", "Prosthetics", "Prosthethiscs" },
                    { new Guid("83b5f96e-f697-45a2-90aa-c81960f930c7"), "SPN", "Spine", "Spine" },
                    { new Guid("a6653cbe-f9fb-41c1-8ef3-5fb59489c6aa"), "ART", "Arthroscopy", "Arthroscopy" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "124dbabe-c25e-4aef-81ee-bfe0160a73d4", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "Doctor" },
                    { "1fcca6ea-560e-4b4f-95a9-c2eb9e6f747a", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "Doctor" },
                    { "b8e61a94-d7a2-4f5d-b75b-ea12858afa5c", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "Nurse" },
                    { "f04b0e9b-f54c-4112-83b3-6fac7f168017", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "Doctor" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "SurgeryRoom",
                columns: new[] { "Id", "AssignedEquipment", "Capacity", "MaintenanceSlots", "Number", "RoomStatus", "RoomType" },
                values: new object[,]
                {
                    { "0ead0319-5a3e-4610-8944-2b812b803222", "[\"Scalpel\",\"Monitor\",\"Table\"]", 10, "[\"28/10/2024=[12:30,13:00];\"]", 201, "Available", "OperatingRoom" },
                    { "e70c40e1-f1d9-427f-918f-bda7ed12447c", "[\"Scalpel\",\"Monitor\"]", 10, "[\"28/10/2024=[09:30,10:00];\"]", 200, "Available", "OperatingRoom" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d31d8ad6-2a97-4c27-8b1c-106c393cd810", "31771157-4697-4766-9f35-aa09c696c793" },
                    { "9b4c17cd-5d67-4a04-b66b-4c3d283309e1", "a4e34928-0b86-4c39-9087-42f92865c19f" },
                    { "64c8e920-3960-46fa-8e8c-8b20203c59aa", "b0974e73-c88a-4c10-b2cb-2f96a45f27f9" },
                    { "d31d8ad6-2a97-4c27-8b1c-106c393cd810", "cf6e441b-d007-4655-bcbc-ccbdc62a8852" },
                    { "638e2377-9757-472c-a498-1038e80b6ec1", "da187419-b6c3-4be4-9221-c9123337ca99" },
                    { "d31d8ad6-2a97-4c27-8b1c-106c393cd810", "eb64b6ff-2213-4a40-898a-7bdcf964859e" },
                    { "34b9fa00-a5ce-416a-8686-ad4a93f78f16", "f3dbe6eb-aa9a-455d-b2b0-01b953a7a518" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("036120f9-8e2c-4195-a436-39d0dac9922e"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fe672ae2-2da1-4fbb-9d9c-d9cb80f230d8"), "ba953560-2def-4f6e-b87a-60f8fac08a79", "Urgent", "Pending", "1fcca6ea-560e-4b4f-95a9-c2eb9e6f747a" },
                    { new Guid("0b93cdf4-e67d-46b5-90f3-aa04bdd0b387"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6b16357-d72d-4e11-b48b-c6bfaac52fd8"), "50dfebcf-3578-48d1-b868-5427d851bb72", "Emergency", "Pending", "f04b0e9b-f54c-4112-83b3-6fac7f168017" },
                    { new Guid("1a27bb89-2f10-4458-9755-4bbd1fec4c9c"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e359c70-6cf5-449c-9e73-c8745f867beb"), "9fc45861-7910-44d5-93c0-7893f4659e9b", "Elective", "Pending", "124dbabe-c25e-4aef-81ee-bfe0160a73d4" },
                    { new Guid("23810870-7bf5-4785-b566-6b3a902efe95"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0ec1ad77-7bde-4afb-a83c-498ba89ec9b8"), "88acd096-79e8-415c-9ce9-0757c2597a84", "Elective", "Pending", "1fcca6ea-560e-4b4f-95a9-c2eb9e6f747a" },
                    { new Guid("23f7701f-6b2c-4c9d-88bd-5ff3ed0af70d"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c6a49f17-8071-4db5-9d6a-deecfed61604"), "9405519c-d200-404e-af58-82da32c9f249", "Urgent", "Pending", "124dbabe-c25e-4aef-81ee-bfe0160a73d4" },
                    { new Guid("2857c76d-7c12-48a7-b010-bb640e643dd1"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0e359c70-6cf5-449c-9e73-c8745f867beb"), "9fc45861-7910-44d5-93c0-7893f4659e9b", "Emergency", "Pending", "1fcca6ea-560e-4b4f-95a9-c2eb9e6f747a" },
                    { new Guid("2ff3b742-b9b2-405f-ba12-50f2afdedfc2"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bad19991-3e1c-4424-bd46-b86d0e04a140"), "d8b6071f-580b-4cf3-aa59-803ca83f6dfe", "Urgent", "Pending", "f04b0e9b-f54c-4112-83b3-6fac7f168017" },
                    { new Guid("30c8ba54-db23-4b77-bf44-2235402f5590"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8beac4ef-1a57-40df-8d42-200c155c3606"), "7fe6ebee-1d7e-45d1-8053-c77635b3c273", "Urgent", "Pending", "124dbabe-c25e-4aef-81ee-bfe0160a73d4" },
                    { new Guid("3b25fd3b-8ea0-4ea1-9dda-f1ce15ddcea9"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0ec1ad77-7bde-4afb-a83c-498ba89ec9b8"), "1405015e-7294-49a3-bb5a-c8a8f09ecf06", "Emergency", "Pending", "f04b0e9b-f54c-4112-83b3-6fac7f168017" },
                    { new Guid("3e229425-bdfe-4f68-a1d2-8fcd95ce1e62"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8beac4ef-1a57-40df-8d42-200c155c3606"), "5f94b8f7-1a1b-4451-82ff-a1c9bc3d4b51", "Urgent", "Pending", "f04b0e9b-f54c-4112-83b3-6fac7f168017" },
                    { new Guid("733aba27-8938-4b08-a2af-65c26e2af8a4"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1266af33-ed44-4419-8879-652e2a0ff073"), "728f61fb-132e-408d-b1bd-bc7aa6fad1fd", "Elective", "Pending", "f04b0e9b-f54c-4112-83b3-6fac7f168017" },
                    { new Guid("90aba6e5-7840-482d-b82f-1d093f6d8449"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1266af33-ed44-4419-8879-652e2a0ff073"), "a2fabed7-82ce-4e68-b207-fe91f9d33ec8", "Emergency", "Pending", "1fcca6ea-560e-4b4f-95a9-c2eb9e6f747a" },
                    { new Guid("af350abc-7ce1-41c8-a297-e009667fc6d8"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0529c64a-3ccd-4d59-8bb5-563c07064ec3"), "7c72d74c-f3dd-4c29-a332-1988018074fb", "Emergency", "Pending", "124dbabe-c25e-4aef-81ee-bfe0160a73d4" },
                    { new Guid("d81e1042-d599-48fe-8628-4a89a86b2e50"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f6b16357-d72d-4e11-b48b-c6bfaac52fd8"), "1405015e-7294-49a3-bb5a-c8a8f09ecf06", "Urgent", "Pending", "124dbabe-c25e-4aef-81ee-bfe0160a73d4" },
                    { new Guid("dd7c6a34-6867-470d-9f17-678e5a3aa7bf"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bad19991-3e1c-4424-bd46-b86d0e04a140"), "474193fe-6b54-4d9d-b4d2-db3bda005660", "Emergency", "Pending", "124dbabe-c25e-4aef-81ee-bfe0160a73d4" },
                    { new Guid("fa87cbac-2f0b-43d0-abe9-af05708e7b81"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("65b662ff-eb3a-410f-be57-c6ddf1476de2"), "728f61fb-132e-408d-b1bd-bc7aa6fad1fd", "Urgent", "Pending", "1fcca6ea-560e-4b4f-95a9-c2eb9e6f747a" },
                    { new Guid("fde9564f-6325-4304-bf4e-56f5e79f5f10"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0529c64a-3ccd-4d59-8bb5-563c07064ec3"), "474193fe-6b54-4d9d-b4d2-db3bda005660", "Elective", "Pending", "f04b0e9b-f54c-4112-83b3-6fac7f168017" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Appointment",
                columns: new[] { "Id", "AppointmentStatus", "DateTime", "OperationRequestId", "SurgeryRoomId" },
                values: new object[,]
                {
                    { new Guid("26be6a44-be6b-4645-929e-877389d2fdac"), "Scheduled", "28/10/2024 10:30:00", new Guid("fde9564f-6325-4304-bf4e-56f5e79f5f10"), "e70c40e1-f1d9-427f-918f-bda7ed12447c" },
                    { new Guid("f133f267-93b5-4004-a4b2-9e4b41e6f90d"), "Scheduled", "28/10/2024 18:30:00", new Guid("3e229425-bdfe-4f68-a1d2-8fcd95ce1e62"), "0ead0319-5a3e-4610-8944-2b812b803222" }
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
