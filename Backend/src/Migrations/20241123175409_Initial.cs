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
                    Specialization = table.Column<int>(type: "int", nullable: false),
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
                    { "02279d1c-4975-4f8b-8ff9-8ec403d38176", null, "Doctor", "DOCTOR" },
                    { "0c68aa9d-bda0-45d6-9f9a-879937a5bb78", null, "Patient", "PATIENT" },
                    { "2a7737ca-0fb4-4ddb-ab30-01a596aae769", null, "Nurse", "NURSE" },
                    { "6165e57f-df8a-4dd2-a45a-c345d67f96d1", null, "Admin", "ADMIN" },
                    { "81221ae1-8f48-49d1-a709-f2052ceab30b", null, "Technician", "TECHNICIAN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0de4af34-cafb-4343-94dd-2186a675cd77", 0, "9183281b-8080-4449-a0ca-5d0fed155a23", "tiago@hospital.com", true, false, null, "TIAGO@HOSPITAL.COM", "TIAGO", "AQAAAAIAAYagAAAAELpU7YZx1iFZ0vbmXSnUaL+pfEGovfzRntSEX5PbMukCZAEepj6Hno3efFl2X/LZlA==", null, false, "6f789fee-b80a-479f-bfb0-f8a206ef6f03", false, "tiago" },
                    { "23df366d-e149-436c-af51-ea92f4f79bca", 0, "871493d4-4118-43d8-8ce4-916d8909b1da", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEFqLooalaY/cIwt7ZfOkGzQl4+DggsiV7dPUS9zX14IKLYtmh+oqADDgEWDm+LnRWw==", null, false, "beeff01f-a688-407c-9550-3ca175bc80e5", false, "patient" },
                    { "608bff0e-0b3b-4ed6-9b79-1985e6933407", 0, "07b8ec4a-fae4-4e9a-b7c3-64b350e78f16", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEKqd138uxff9izBeAMLdkfgoeeCjtAnvl3I+tBTGiI+EpIqOrcKaIrAUfLN9IFOF7g==", null, false, "45968c90-a335-424f-a33c-91657e5bdd60", false, "admin" },
                    { "7d5a6b27-c656-4d41-b6da-3dc70044a800", 0, "d3dccd9f-d93c-4682-b806-184cbf1555fb", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEFoiiHUAB5/+iS+6o5/WFZJL4+M02ZiMO2oEIj4aIEmLmEfQDS89xfeH9IJx84bxwg==", null, false, "f9984b80-1dd9-45ad-bad8-c6c60cecfb7b", false, "doctor" },
                    { "939446d3-a0e6-4eaf-8168-522f2208e7d2", 0, "8d2131f3-9126-4a71-8cdc-8ee0da4d4451", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEGfbSapvmoi7SJNABIHN41k3CrwNEEWkSBczvI05sqxBp5KhalwVSFavZarI+XqFFw==", null, false, "f8b0bf17-3b4c-4a33-ac8a-ec500b997f0c", false, "nurse" },
                    { "ccc6bba8-16f2-404a-915f-2f09af15c440", 0, "9cee1629-9c06-45a7-8138-173009a61e44", "andre@hospital.com", true, false, null, "ANDRE@HOSPITAL.COM", "ANDRE", "AQAAAAIAAYagAAAAENtSigLyyFnmL2xu5uJHljQBAVMjlRTJromofh/bk7uKh5nVYj+v0sFnHHpYt9Fnlw==", null, false, "fefbd64b-6be9-4fde-8679-4b76bf861368", false, "andre" },
                    { "e3dd1eb3-4cb4-4dce-90c9-4da467a5632c", 0, "378f49c9-9c88-447d-833d-33b9b3038b65", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEDbVmkV0BhBjz1cY3+Wdc8hA35IRWAsSUdH7VDcE23HaacQ+c2Uot7GjGnMRdhvIKQ==", null, false, "4223af48-27d4-4216-8cc2-4adde07b89d1", false, "technician" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "Specialization", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("11569f84-f772-455b-ae9a-f3960d8ab301"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 20, 15, "Lumbar disectomy", 45 },
                    { new Guid("190de172-aa31-46fa-9071-89b7dcfa33eb"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 15, 15, "Trigger finger", 10 },
                    { new Guid("3373623d-7493-453c-867e-07706d262b49"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 20, "Meniscal inury treatment", 45 },
                    { new Guid("34580b4b-dba7-405b-8d0e-3db226c7dce3"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 15, 15, "Carpal tunnel syndrome", 10 },
                    { new Guid("5be1eeff-86cf-43e2-ad23-57f2d126efcc"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 30, "ACL reconstruction", 60 },
                    { new Guid("74e872a3-c709-4caf-8616-dbf5e25aa5c6"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 30, 20, "Ankle ligaments repair", 45 },
                    { new Guid("90ee776b-9352-4478-a88c-767bc523c99f"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 45, "Knee replacement", 60 },
                    { new Guid("a589a07b-2b43-4d4c-a3bd-5769882c900f"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 45, "Shoulder replacement", 90 },
                    { new Guid("d135f6e0-1795-4f74-9208-d3191a979ea1"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 45, "Hip replacement", 75 },
                    { new Guid("e79176e5-6ba6-44a9-8216-42516071d114"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 30, "Rotator cuff repair", 80 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "04b31da2-6a8f-4e4b-b0b4-58ca52d480b7", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", null },
                    { "1b2bb478-baed-4340-9fdc-550053e330fb", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", null },
                    { "1c585d44-c2a0-4f2f-84b2-613b7e05e818", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", null },
                    { "2dfb9c00-7649-4959-a358-2ab2e7dfd6dc", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", null },
                    { "5a454574-05fb-4ddc-95ca-d0751dbc9684", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", null },
                    { "61b6228e-8ed5-4971-8e98-876868cee92a", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", null },
                    { "91db188b-c49f-4d2f-a6ef-2baa22a14c7d", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", null },
                    { "c2cb59f0-6316-44d7-995a-fa53eeac91c7", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", null },
                    { "c7d49b15-07ce-4a1e-9334-64459ca55c04", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", null },
                    { "ccc82c0a-6b04-4607-aca9-3c40bb58f562", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", null },
                    { "d10a2e5b-2492-43a8-90fe-1c99bd8a1f7a", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", null },
                    { "d5ea5a3c-7599-49df-9881-563a478dba5b", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", null },
                    { "fac1296d-f510-4253-a5ea-b5c21d6a1523", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", null }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "76f6f204-409d-4a96-b1db-8aa3ce0e3546", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "Doctor" },
                    { "7c3f3690-7d7c-43d2-83be-7c0a13f436df", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "Nurse" },
                    { "eb6cf2af-3fab-498f-a39a-2c1359b5122c", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "Doctor" },
                    { "f40ff59c-c1a5-4b0e-a6e2-d0e18bad2931", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "Doctor" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "SurgeryRoom",
                columns: new[] { "Id", "AssignedEquipment", "Capacity", "MaintenanceSlots", "Number", "RoomStatus", "RoomType" },
                values: new object[,]
                {
                    { "843017e8-81bb-4596-acf3-58af0c0ed0f5", "[\"Scalpel\",\"Monitor\",\"Table\"]", 10, "[\"28/10/2024=[12:30,13:00];\"]", 201, "Available", "OperatingRoom" },
                    { "dc8b204b-bc8c-4dc0-a9f6-2cc340670850", "[\"Scalpel\",\"Monitor\"]", 10, "[\"28/10/2024=[09:30,10:00];\"]", 200, "Available", "OperatingRoom" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "02279d1c-4975-4f8b-8ff9-8ec403d38176", "0de4af34-cafb-4343-94dd-2186a675cd77" },
                    { "0c68aa9d-bda0-45d6-9f9a-879937a5bb78", "23df366d-e149-436c-af51-ea92f4f79bca" },
                    { "6165e57f-df8a-4dd2-a45a-c345d67f96d1", "608bff0e-0b3b-4ed6-9b79-1985e6933407" },
                    { "02279d1c-4975-4f8b-8ff9-8ec403d38176", "7d5a6b27-c656-4d41-b6da-3dc70044a800" },
                    { "2a7737ca-0fb4-4ddb-ab30-01a596aae769", "939446d3-a0e6-4eaf-8168-522f2208e7d2" },
                    { "02279d1c-4975-4f8b-8ff9-8ec403d38176", "ccc6bba8-16f2-404a-915f-2f09af15c440" },
                    { "81221ae1-8f48-49d1-a709-f2052ceab30b", "e3dd1eb3-4cb4-4dce-90c9-4da467a5632c" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("13348ffb-be71-4824-b71d-95fabd758704"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("34580b4b-dba7-405b-8d0e-3db226c7dce3"), "1b2bb478-baed-4340-9fdc-550053e330fb", "Urgent", "Pending", "76f6f204-409d-4a96-b1db-8aa3ce0e3546" },
                    { new Guid("1812fe31-0f9a-4530-9c2b-3403822a107b"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a589a07b-2b43-4d4c-a3bd-5769882c900f"), "c7d49b15-07ce-4a1e-9334-64459ca55c04", "Emergency", "Pending", "eb6cf2af-3fab-498f-a39a-2c1359b5122c" },
                    { new Guid("215b4ab7-9fa6-46c0-b629-889ce84e1677"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d135f6e0-1795-4f74-9208-d3191a979ea1"), "c2cb59f0-6316-44d7-995a-fa53eeac91c7", "Urgent", "Pending", "eb6cf2af-3fab-498f-a39a-2c1359b5122c" },
                    { new Guid("262daf1a-1023-4ba7-9656-583ca628b690"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a589a07b-2b43-4d4c-a3bd-5769882c900f"), "04b31da2-6a8f-4e4b-b0b4-58ca52d480b7", "Urgent", "Pending", "76f6f204-409d-4a96-b1db-8aa3ce0e3546" },
                    { new Guid("408410c9-822a-4f3f-adbe-602797c2ca4a"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5be1eeff-86cf-43e2-ad23-57f2d126efcc"), "1c585d44-c2a0-4f2f-84b2-613b7e05e818", "Elective", "Pending", "eb6cf2af-3fab-498f-a39a-2c1359b5122c" },
                    { new Guid("5fe23ad0-8b90-43d6-b8d0-fe608d3f16ca"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90ee776b-9352-4478-a88c-767bc523c99f"), "5a454574-05fb-4ddc-95ca-d0751dbc9684", "Urgent", "Pending", "eb6cf2af-3fab-498f-a39a-2c1359b5122c" },
                    { new Guid("6cbe091a-a745-4240-a6ae-11daed8fef6a"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("190de172-aa31-46fa-9071-89b7dcfa33eb"), "91db188b-c49f-4d2f-a6ef-2baa22a14c7d", "Elective", "Pending", "76f6f204-409d-4a96-b1db-8aa3ce0e3546" },
                    { new Guid("6eba10b2-53f5-402b-84b6-f976cbbaa933"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("190de172-aa31-46fa-9071-89b7dcfa33eb"), "91db188b-c49f-4d2f-a6ef-2baa22a14c7d", "Emergency", "Pending", "f40ff59c-c1a5-4b0e-a6e2-d0e18bad2931" },
                    { new Guid("70717e71-c160-464c-899c-b4d7bc1c6ee1"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11569f84-f772-455b-ae9a-f3960d8ab301"), "ccc82c0a-6b04-4607-aca9-3c40bb58f562", "Urgent", "Pending", "f40ff59c-c1a5-4b0e-a6e2-d0e18bad2931" },
                    { new Guid("8a0e8888-3dc8-4b12-a135-46af707023b4"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("74e872a3-c709-4caf-8616-dbf5e25aa5c6"), "2dfb9c00-7649-4959-a358-2ab2e7dfd6dc", "Emergency", "Pending", "f40ff59c-c1a5-4b0e-a6e2-d0e18bad2931" },
                    { new Guid("8a9d87d8-0b07-4e15-b24c-42b17e29852a"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3373623d-7493-453c-867e-07706d262b49"), "04b31da2-6a8f-4e4b-b0b4-58ca52d480b7", "Emergency", "Pending", "eb6cf2af-3fab-498f-a39a-2c1359b5122c" },
                    { new Guid("add1b13e-9e22-4080-be2c-ffabe7fc648d"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d135f6e0-1795-4f74-9208-d3191a979ea1"), "1c585d44-c2a0-4f2f-84b2-613b7e05e818", "Emergency", "Pending", "76f6f204-409d-4a96-b1db-8aa3ce0e3546" },
                    { new Guid("af836e19-3ae1-417f-943e-a60a9b7add31"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("74e872a3-c709-4caf-8616-dbf5e25aa5c6"), "d10a2e5b-2492-43a8-90fe-1c99bd8a1f7a", "Elective", "Pending", "eb6cf2af-3fab-498f-a39a-2c1359b5122c" },
                    { new Guid("b05eee89-cf26-4dc5-99ef-40fe7b489a96"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("90ee776b-9352-4478-a88c-767bc523c99f"), "fac1296d-f510-4253-a5ea-b5c21d6a1523", "Urgent", "Pending", "76f6f204-409d-4a96-b1db-8aa3ce0e3546" },
                    { new Guid("b8c4f505-fc24-4d5f-be53-e84b4f3d4a32"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e79176e5-6ba6-44a9-8216-42516071d114"), "d10a2e5b-2492-43a8-90fe-1c99bd8a1f7a", "Urgent", "Pending", "f40ff59c-c1a5-4b0e-a6e2-d0e18bad2931" },
                    { new Guid("d578615c-41c5-4b20-91ff-0636bc84b93c"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5be1eeff-86cf-43e2-ad23-57f2d126efcc"), "61b6228e-8ed5-4971-8e98-876868cee92a", "Emergency", "Pending", "76f6f204-409d-4a96-b1db-8aa3ce0e3546" },
                    { new Guid("f728510c-1382-4824-b6b2-2f528c5891aa"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3373623d-7493-453c-867e-07706d262b49"), "d5ea5a3c-7599-49df-9881-563a478dba5b", "Elective", "Pending", "f40ff59c-c1a5-4b0e-a6e2-d0e18bad2931" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Appointment",
                columns: new[] { "Id", "AppointmentStatus", "DateTime", "OperationRequestId", "SurgeryRoomId" },
                values: new object[,]
                {
                    { new Guid("2bcb3307-5dc4-4bc3-ae93-fe8b9ed09412"), "Scheduled", "28/10/2024 10:30:00", new Guid("408410c9-822a-4f3f-adbe-602797c2ca4a"), "dc8b204b-bc8c-4dc0-a9f6-2cc340670850" },
                    { new Guid("dd5fe3ce-bf6d-46a8-829c-a57625251ca3"), "Scheduled", "28/10/2024 18:30:00", new Guid("5fe23ad0-8b90-43d6-b8d0-fe608d3f16ca"), "843017e8-81bb-4596-acf3-58af0c0ed0f5" }
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
        }
    }
}
