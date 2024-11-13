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
                    AssignedEquipment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaintenanceSlots = table.Column<string>(type: "longtext", nullable: true)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4dfb755c-603e-40c3-a4dd-c7a6110b311e", null, "Doctor", "DOCTOR" },
                    { "5aaa47f1-6087-40c0-8b6a-f5216ffda21d", null, "Patient", "PATIENT" },
                    { "aadd5d4c-73b8-4a6f-adc9-82da57b8deb8", null, "Nurse", "NURSE" },
                    { "b4d0b3f9-d373-449c-8a63-2a1ff329edf3", null, "Admin", "ADMIN" },
                    { "dbfdb3b2-2f91-45bb-b8d6-daf635164bf2", null, "Technician", "TECHNICIAN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4421d1af-9062-4324-a841-68567445815b", 0, "3a86c993-6468-4e1a-81f1-e418d230fb66", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEHcs8y0UA52/dw1eghmob+zUHUkW4Rg3OOzJlHFVJCti3VHj2gWpkj3xlEgDhxKsQg==", null, false, "aa00f85f-0567-4bae-a6f9-3addc0edda2f", false, "technician" },
                    { "5e004765-e73d-46ca-8b88-4b596237a0c9", 0, "0c30359d-aeaf-4919-9bfa-1eced7740d98", "tiago@hospital.com", true, false, null, "TIAGO@HOSPITAL.COM", "TIAGO", "AQAAAAIAAYagAAAAEEtv7kxS20PvSJtXaJHMjocm1AH9WpuObPqocW4ppKmIjME+Ty4kR8rTkOaKnpB4LQ==", null, false, "718b1b3d-4e3b-4433-bb22-cd10dd6740c6", false, "tiago" },
                    { "60f0ec0b-17a1-44ee-90e7-26ca2cf5f146", 0, "1a907c8c-ce3c-42cd-80e8-895c90637474", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEDdhpAgv1/WRBIeLveU4qxyCIm/Cprtvy+4Oz04qoHeA27rdLnMggA2hYIRL9qRZeQ==", null, false, "459912ae-600c-4059-923f-612111467b03", false, "admin" },
                    { "62d76af6-dfde-4c58-92c1-04b609783f19", 0, "6c1386b0-4407-4295-9810-f60731b5d384", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAENdJAWqQ+uKjwBsOEi+SgZTh/vJvFrOolsV3hAeT+xvrbSMhdO3aXGZxbrkAsiK5Hg==", null, false, "75e9f234-96dc-4151-a565-eb45af617b8b", false, "nurse" },
                    { "8480957f-1e8c-4ae1-8e4f-9630eb7410db", 0, "c035b668-a830-4ecb-826d-75f9d03d42ee", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEMdGmBA/iXG11H53odRu9OoFmqWPRsN9WUrcOwRHFC4T8846j/dQ4laEVu/EPgRV/w==", null, false, "579ad397-f334-488a-9f48-cb9c1bc80503", false, "patient" },
                    { "cfd4792d-a180-413c-937e-f03e9b794508", 0, "13018ce3-4a66-4169-8333-d8c190bd8395", "andre@hospital.com", true, false, null, "ANDRE@HOSPITAL.COM", "ANDRE", "AQAAAAIAAYagAAAAELm0hyJr/WNTx63UIy+jxnv5sPT/U8qwDD6oXLDX67y7UVEoffPrlWCl+yPL/rGhaA==", null, false, "7e8363d0-3b84-4252-8a1d-04e231d484ae", false, "andre" },
                    { "d1ca3ada-1a1f-4bb4-962c-1855190a5538", 0, "9d74f32a-4fa5-493d-a897-d7284cca090e", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEEgX1Ibqs7cwyNKtb30aAMeCVjmXvnVixWdGn4doYE/+DT5rC8JnJIFbGAiJngLz9w==", null, false, "d40beeb0-0459-4e82-a553-ab05dc093fda", false, "doctor" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "Specialization", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("0fc3172b-a55d-48c0-97fa-188275fd7ba6"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Carpal tunnel syndrome", 0 },
                    { new Guid("51c93387-09e3-468d-8bd0-a28c5a880698"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Meniscal inury treatment", 0 },
                    { new Guid("6a1057b5-bcdb-48af-9bcf-01814e78fe7d"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Lumbar disectomy", 0 },
                    { new Guid("7b687618-0a4c-4e72-ad71-341ce3143c75"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Rotator cuff repair", 0 },
                    { new Guid("8a31397a-2162-44f5-a3c0-a7e1b2232bc8"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Shoulder replacement", 0 },
                    { new Guid("8f8ba1db-a33a-4603-a287-7645d2af0aa0"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Hip replacement", 0 },
                    { new Guid("b26fe3f6-c1f9-42a9-b4ad-05b4c2d73b7f"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Ankle ligaments repair", 0 },
                    { new Guid("b892be18-edd8-4174-801c-fe5f4874eda1"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "ACL reconstruction", 0 },
                    { new Guid("df63be30-f855-499d-83b9-7ffebafa43c6"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Knee replacement", 0 },
                    { new Guid("ffc431a8-9f3a-4862-85aa-9c72b78a6300"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Trigger finger", 0 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "0ca2d8f1-7f58-4111-afda-defba4bd205e", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", null },
                    { "28e27aee-4071-4884-a2f5-a8773e5fbbdc", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", null },
                    { "3fab474d-a02e-42ad-a8c9-eb33c14af459", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", null },
                    { "44b4fcf1-9640-4bb4-8c69-a04a19c8cf82", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", null },
                    { "47942037-9797-4676-bae0-f13128e523cf", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", null },
                    { "6fccd1f1-3bbe-4f93-b515-c873b742c64c", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", null },
                    { "a7764fa2-82e7-4849-a816-9375dc0d1a69", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", null },
                    { "b0941d54-935d-478b-a9a2-3c12f5788bc6", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", null },
                    { "b26fcf60-8eff-4f08-abdf-25c50ef1cfb9", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", null },
                    { "c09c9241-bc52-4fcb-b1e4-3f803d0357fb", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", null },
                    { "d746a831-192b-4a54-bacd-b0c592fab6fa", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", null },
                    { "f60a31c2-3919-4af4-8108-730ef3231f42", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", null },
                    { "ff590868-295d-4b08-a5af-37c4de024da8", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", null }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "69e93642-9ecf-4e13-a6bd-f9e090e51b18", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "Nurse" },
                    { "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "Doctor" },
                    { "9a960f09-ef65-4ac1-8590-9450018decff", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "Doctor" },
                    { "df68ef61-e764-4f01-8da0-aaafa17ea62e", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "Doctor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "dbfdb3b2-2f91-45bb-b8d6-daf635164bf2", "4421d1af-9062-4324-a841-68567445815b" },
                    { "4dfb755c-603e-40c3-a4dd-c7a6110b311e", "5e004765-e73d-46ca-8b88-4b596237a0c9" },
                    { "b4d0b3f9-d373-449c-8a63-2a1ff329edf3", "60f0ec0b-17a1-44ee-90e7-26ca2cf5f146" },
                    { "aadd5d4c-73b8-4a6f-adc9-82da57b8deb8", "62d76af6-dfde-4c58-92c1-04b609783f19" },
                    { "5aaa47f1-6087-40c0-8b6a-f5216ffda21d", "8480957f-1e8c-4ae1-8e4f-9630eb7410db" },
                    { "4dfb755c-603e-40c3-a4dd-c7a6110b311e", "cfd4792d-a180-413c-937e-f03e9b794508" },
                    { "4dfb755c-603e-40c3-a4dd-c7a6110b311e", "d1ca3ada-1a1f-4bb4-962c-1855190a5538" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("02d85950-7cf6-473e-a1fb-4ab82053c054"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("51c93387-09e3-468d-8bd0-a28c5a880698"), "47942037-9797-4676-bae0-f13128e523cf", "Elective", "Pending", "9a960f09-ef65-4ac1-8590-9450018decff" },
                    { new Guid("1918757b-5e34-4339-846f-9d9a065bf746"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b892be18-edd8-4174-801c-fe5f4874eda1"), "3fab474d-a02e-42ad-a8c9-eb33c14af459", "Elective", "Pending", "df68ef61-e764-4f01-8da0-aaafa17ea62e" },
                    { new Guid("21a895be-79ea-46a9-8415-b4efedcdc18f"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b892be18-edd8-4174-801c-fe5f4874eda1"), "b26fcf60-8eff-4f08-abdf-25c50ef1cfb9", "Emergency", "Pending", "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4" },
                    { new Guid("21c38021-0f8c-4c16-937a-9a922dd45c94"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ffc431a8-9f3a-4862-85aa-9c72b78a6300"), "28e27aee-4071-4884-a2f5-a8773e5fbbdc", "Emergency", "Pending", "9a960f09-ef65-4ac1-8590-9450018decff" },
                    { new Guid("37f18ca6-4dc7-43eb-85be-8cfff8c7cc9c"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6a1057b5-bcdb-48af-9bcf-01814e78fe7d"), "6fccd1f1-3bbe-4f93-b515-c873b742c64c", "Urgent", "Pending", "9a960f09-ef65-4ac1-8590-9450018decff" },
                    { new Guid("383c357b-bc2a-4280-86ba-91adbc3d0374"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7b687618-0a4c-4e72-ad71-341ce3143c75"), "d746a831-192b-4a54-bacd-b0c592fab6fa", "Urgent", "Pending", "9a960f09-ef65-4ac1-8590-9450018decff" },
                    { new Guid("4b2261a5-b0b4-4238-814a-8515cabcc0f1"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8f8ba1db-a33a-4603-a287-7645d2af0aa0"), "ff590868-295d-4b08-a5af-37c4de024da8", "Urgent", "Pending", "df68ef61-e764-4f01-8da0-aaafa17ea62e" },
                    { new Guid("50a38822-f709-4979-96c3-4f9d79515e80"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b26fe3f6-c1f9-42a9-b4ad-05b4c2d73b7f"), "d746a831-192b-4a54-bacd-b0c592fab6fa", "Elective", "Pending", "df68ef61-e764-4f01-8da0-aaafa17ea62e" },
                    { new Guid("52ad58b3-f69e-4af5-a044-585535a19372"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("df63be30-f855-499d-83b9-7ffebafa43c6"), "a7764fa2-82e7-4849-a816-9375dc0d1a69", "Urgent", "Pending", "df68ef61-e764-4f01-8da0-aaafa17ea62e" },
                    { new Guid("560aa8df-c989-46c8-a45c-aa8555b9134f"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a31397a-2162-44f5-a3c0-a7e1b2232bc8"), "0ca2d8f1-7f58-4111-afda-defba4bd205e", "Emergency", "Pending", "df68ef61-e764-4f01-8da0-aaafa17ea62e" },
                    { new Guid("75b40f62-2e54-4b7d-9584-245e2f691e69"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b26fe3f6-c1f9-42a9-b4ad-05b4c2d73b7f"), "f60a31c2-3919-4af4-8108-730ef3231f42", "Emergency", "Pending", "9a960f09-ef65-4ac1-8590-9450018decff" },
                    { new Guid("88a295a8-47a5-43e8-a52a-3c5cfc0dd1af"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("df63be30-f855-499d-83b9-7ffebafa43c6"), "44b4fcf1-9640-4bb4-8c69-a04a19c8cf82", "Urgent", "Pending", "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4" },
                    { new Guid("b6d80047-0f4c-4f36-8985-8f7040cf79ed"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a31397a-2162-44f5-a3c0-a7e1b2232bc8"), "b0941d54-935d-478b-a9a2-3c12f5788bc6", "Urgent", "Pending", "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4" },
                    { new Guid("caaeb7e0-f503-4b64-85fe-00dd8ed1aa97"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0fc3172b-a55d-48c0-97fa-188275fd7ba6"), "c09c9241-bc52-4fcb-b1e4-3f803d0357fb", "Urgent", "Pending", "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4" },
                    { new Guid("dd1d256e-e5ec-4f1b-bb68-627108ac18c6"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ffc431a8-9f3a-4862-85aa-9c72b78a6300"), "28e27aee-4071-4884-a2f5-a8773e5fbbdc", "Elective", "Pending", "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4" },
                    { new Guid("e6197ee3-26fd-4762-8d60-abdf72daecca"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8f8ba1db-a33a-4603-a287-7645d2af0aa0"), "3fab474d-a02e-42ad-a8c9-eb33c14af459", "Emergency", "Pending", "9a481f2c-fd27-4797-ba7c-cccde4c9e7f4" },
                    { new Guid("fb0b7659-e0d5-4d85-8a14-ee26dbed0a65"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("51c93387-09e3-468d-8bd0-a28c5a880698"), "b0941d54-935d-478b-a9a2-3c12f5788bc6", "Emergency", "Pending", "df68ef61-e764-4f01-8da0-aaafa17ea62e" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_PatientId",
                table: "Allergy",
                column: "PatientId");

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
