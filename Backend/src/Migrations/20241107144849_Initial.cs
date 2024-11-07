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
                    { "26e73359-400e-4a14-93d6-896b129d4c6a", null, "Doctor", "DOCTOR" },
                    { "56a5fd77-7c2d-4f8d-9439-875a97137136", null, "Nurse", "NURSE" },
                    { "846cb3f0-2ecf-4930-81a3-061353b6109c", null, "Patient", "PATIENT" },
                    { "92cc3254-c3f0-476c-826f-cfb3c62c3b58", null, "Admin", "ADMIN" },
                    { "ff8daed2-266a-493b-b9f8-9fcefd4ea65d", null, "Technician", "TECHNICIAN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1ec3bffe-447a-430c-862c-c4c4cad872a9", 0, "489e9913-38ac-486d-8e6d-d809f94cd4a5", "tiago@hospital.com", true, false, null, "TIAGO@HOSPITAL.COM", "TIAGO", "AQAAAAIAAYagAAAAEBL34F5uMZWVtvpVcjQXkqzJCycvXZHdpfsRKGiRn709D0aAuyL8wXnUU5c2F3bf2Q==", null, false, "3ff67abc-82c6-46c5-9605-cfdbc85b2d67", false, "tiago" },
                    { "301cd8fa-5824-40e4-bdb0-6e9b379327d3", 0, "79a5c1da-48a8-4289-be21-c9ec717a07d1", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEGNCwv+vfpg0VXUXoMfo2AF2VtLhm5Kn5H0oABKnuYHI+dE8nwzV7yJEEmc+kp/0rA==", null, false, "8a972dba-14b6-424f-8ad3-827bd0289c08", false, "technician" },
                    { "9ef7880d-7f18-471f-99b4-a442cc9a9c5f", 0, "5dc0ad34-e6ac-4dc4-a597-c4e8ad101c4e", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEAF9lBTNa3YH3Q8bmH/FUvFfvdPFzNF2aRJ4RQd8HdJbZYdZ/j9wthmHUF/obL7Hug==", null, false, "092476d1-447a-4f92-bfcd-d84b99bff213", false, "doctor" },
                    { "a41ce558-f4e0-43f6-845a-047de269f12f", 0, "34c9f784-16ee-4a11-b6d6-976012508036", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEOT8vUT4OnpdjJ0IzQRKLSZhXV/ZKEyeA+O3lFq//XqmYBLPHp6h9BJBsTV0TSOK4Q==", null, false, "af2f7fa0-f16d-4b33-9789-200c278b4c83", false, "patient" },
                    { "b8806f17-3174-4176-8cea-466237fa5a26", 0, "e54ca693-0e63-4f2b-80ab-556343336ff6", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOLobkPYHHwjA6701Xg0NjfGtGpxWw2VK+B4ormTMdvuigoTV9CrV6CQCE0H2AjtgA==", null, false, "d0660e43-21df-4a52-9d08-dbb487514d59", false, "admin" },
                    { "dece7fe3-b5b8-4e42-93d1-7b4a1545f26d", 0, "d553de6d-6783-45a8-9256-e3f51d9e9c97", "andre@hospital.com", true, false, null, "ANDRE@HOSPITAL.COM", "ANDRE", "AQAAAAIAAYagAAAAEAL+1Kb8FpqhxhOvJocnUZKOVVY7o2+HrED4Fb5IlwS2XhxKRAcALPLnMWiEb9Eztw==", null, false, "d29ad94a-8a46-4bdf-ac6b-955a76544343", false, "andre" },
                    { "fd5c5e08-ee8d-4983-9376-9d2144ef8921", 0, "83d6dcf6-88e7-4c06-8fda-50c77f535a10", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEIDLVptB2Slkb9G3bPof2pcICcUi3WNr2xMlq/Aukjzj6dPZSHEseErYQrdVp7bXXA==", null, false, "f4ba3124-51f6-48e1-874e-7fad3b17d72d", false, "nurse" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "Specialization", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("5aaf8bd5-8a97-40c8-962e-a5d6551f7ab7"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Trigger finger", 0 },
                    { new Guid("768f9fe7-13b1-4c7b-a76c-898eff8cfd63"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Meniscal inury treatment", 0 },
                    { new Guid("8a002308-afb6-4fe4-9717-489d9bd0387c"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Lumbar disectomy", 0 },
                    { new Guid("8c6e2f5c-dd74-4a99-92b0-efc0c69d1e2d"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Knee replacement", 0 },
                    { new Guid("a010f19a-63f0-4ae5-abfa-8c5d5d3d2420"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Hip replacement", 0 },
                    { new Guid("b699d934-e01d-45d5-b93a-bd1dcbb894da"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "ACL reconstruction", 0 },
                    { new Guid("bcbcb982-c762-49de-81d3-221180e5b758"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Carpal tunnel syndrome", 0 },
                    { new Guid("c8950bdf-3dbd-4877-921b-401f7bd6d3d9"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Ankle ligaments repair", 0 },
                    { new Guid("e30bc099-8de0-4324-809b-51caedf0af2e"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Shoulder replacement", 0 },
                    { new Guid("edcc5be9-658f-4338-80f9-9f7e093dc5ea"), 1, 1, 1, 1, 1, 1, 1, 0, "ACTIVE", 0, 0, "Rotator cuff repair", 0 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "0d836a06-980c-435a-8c9b-876dc2d4e824", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", null },
                    { "169e899c-427d-445f-9e86-498cf3b93a97", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", null },
                    { "23fe38e8-72b8-4977-8d95-97cf7bd356f2", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", null },
                    { "3796060f-fa45-4b7b-a949-28300638af40", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", null },
                    { "843d9dab-6f18-4e53-a484-5c38deb9a9a1", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", null },
                    { "9270f807-f414-4aae-bf80-6a75deb4b35e", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", null },
                    { "a01503f6-fa1a-416f-bf5d-20c6d871e21a", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", null },
                    { "a21744a1-6026-4cf5-972e-09fbeff6f68d", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", null },
                    { "a679c1dc-8bca-46b0-8cbf-f85b9f4771bc", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", null },
                    { "c19fafac-e0ad-4603-91a2-ecb786d07a0a", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", null },
                    { "cbd3b811-10f4-4b29-954b-09d09716fb26", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", null },
                    { "e3d96cd0-0f4f-4c7b-8688-f502e050dbc4", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", null },
                    { "f294d339-2e13-4ee5-bef2-77d8f0d7f5b8", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", null }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "59979ffa-a40c-48e0-b76f-17721316301b", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "Doctor" },
                    { "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "Doctor" },
                    { "a0f27a1b-1880-4e65-9290-232cd0293764", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "Doctor" },
                    { "e40f33bd-2291-4bbc-8498-d9da548dbd07", null, null, "nurse", null, null, "Nurse" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "26e73359-400e-4a14-93d6-896b129d4c6a", "1ec3bffe-447a-430c-862c-c4c4cad872a9" },
                    { "ff8daed2-266a-493b-b9f8-9fcefd4ea65d", "301cd8fa-5824-40e4-bdb0-6e9b379327d3" },
                    { "26e73359-400e-4a14-93d6-896b129d4c6a", "9ef7880d-7f18-471f-99b4-a442cc9a9c5f" },
                    { "846cb3f0-2ecf-4930-81a3-061353b6109c", "a41ce558-f4e0-43f6-845a-047de269f12f" },
                    { "92cc3254-c3f0-476c-826f-cfb3c62c3b58", "b8806f17-3174-4176-8cea-466237fa5a26" },
                    { "26e73359-400e-4a14-93d6-896b129d4c6a", "dece7fe3-b5b8-4e42-93d1-7b4a1545f26d" },
                    { "56a5fd77-7c2d-4f8d-9439-875a97137136", "fd5c5e08-ee8d-4983-9376-9d2144ef8921" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("02beb8aa-fb33-48c8-87f0-2437ed4c8ba5"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("edcc5be9-658f-4338-80f9-9f7e093dc5ea"), "a679c1dc-8bca-46b0-8cbf-f85b9f4771bc", "Urgent", "Pending", "a0f27a1b-1880-4e65-9290-232cd0293764" },
                    { new Guid("2f2b2c9e-7cdb-4855-a088-ab3b3ae721d3"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c8950bdf-3dbd-4877-921b-401f7bd6d3d9"), "a679c1dc-8bca-46b0-8cbf-f85b9f4771bc", "Elective", "Pending", "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1" },
                    { new Guid("3215d214-6eb9-442e-ac92-b63ed358994d"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8c6e2f5c-dd74-4a99-92b0-efc0c69d1e2d"), "843d9dab-6f18-4e53-a484-5c38deb9a9a1", "Urgent", "Pending", "59979ffa-a40c-48e0-b76f-17721316301b" },
                    { new Guid("4ab9f686-10ff-4e96-980d-17edc64fd647"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5aaf8bd5-8a97-40c8-962e-a5d6551f7ab7"), "9270f807-f414-4aae-bf80-6a75deb4b35e", "Elective", "Pending", "59979ffa-a40c-48e0-b76f-17721316301b" },
                    { new Guid("637f6045-dd39-4656-b414-1aeb4d5f62d1"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a010f19a-63f0-4ae5-abfa-8c5d5d3d2420"), "c19fafac-e0ad-4603-91a2-ecb786d07a0a", "Urgent", "Pending", "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1" },
                    { new Guid("6e8b430d-4ec0-435d-a72e-4d196c1cf46e"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("768f9fe7-13b1-4c7b-a76c-898eff8cfd63"), "f294d339-2e13-4ee5-bef2-77d8f0d7f5b8", "Emergency", "Pending", "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1" },
                    { new Guid("73c76276-a872-4d8f-8a3b-419a9edd9775"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bcbcb982-c762-49de-81d3-221180e5b758"), "a21744a1-6026-4cf5-972e-09fbeff6f68d", "Urgent", "Pending", "59979ffa-a40c-48e0-b76f-17721316301b" },
                    { new Guid("90265510-ecd9-4a18-ae5b-369508bca05c"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c8950bdf-3dbd-4877-921b-401f7bd6d3d9"), "0d836a06-980c-435a-8c9b-876dc2d4e824", "Emergency", "Pending", "a0f27a1b-1880-4e65-9290-232cd0293764" },
                    { new Guid("95bb8f3c-acb9-4c4f-b76c-cf50152686b3"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e30bc099-8de0-4324-809b-51caedf0af2e"), "f294d339-2e13-4ee5-bef2-77d8f0d7f5b8", "Urgent", "Pending", "59979ffa-a40c-48e0-b76f-17721316301b" },
                    { new Guid("a4a4640c-cc6b-4ea4-bb96-6a4844a8de88"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b699d934-e01d-45d5-b93a-bd1dcbb894da"), "3796060f-fa45-4b7b-a949-28300638af40", "Elective", "Pending", "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1" },
                    { new Guid("ae9a2332-42de-446e-a7f8-95ac9b88ecf0"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b699d934-e01d-45d5-b93a-bd1dcbb894da"), "169e899c-427d-445f-9e86-498cf3b93a97", "Emergency", "Pending", "59979ffa-a40c-48e0-b76f-17721316301b" },
                    { new Guid("b2dd9b21-b534-4482-a24d-b6064e07324b"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8c6e2f5c-dd74-4a99-92b0-efc0c69d1e2d"), "e3d96cd0-0f4f-4c7b-8688-f502e050dbc4", "Urgent", "Pending", "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1" },
                    { new Guid("c803f683-c7b4-4653-9d34-1282c2cac07b"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5aaf8bd5-8a97-40c8-962e-a5d6551f7ab7"), "9270f807-f414-4aae-bf80-6a75deb4b35e", "Emergency", "Pending", "a0f27a1b-1880-4e65-9290-232cd0293764" },
                    { new Guid("d36e7ac2-4e98-4062-bcf6-ddad882a58b7"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a010f19a-63f0-4ae5-abfa-8c5d5d3d2420"), "3796060f-fa45-4b7b-a949-28300638af40", "Emergency", "Pending", "59979ffa-a40c-48e0-b76f-17721316301b" },
                    { new Guid("f0a217a7-146c-4136-aaee-4b561e982899"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("768f9fe7-13b1-4c7b-a76c-898eff8cfd63"), "a01503f6-fa1a-416f-bf5d-20c6d871e21a", "Elective", "Pending", "a0f27a1b-1880-4e65-9290-232cd0293764" },
                    { new Guid("f0cbca03-5267-4cc0-8d44-3c8a5124d0aa"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a002308-afb6-4fe4-9717-489d9bd0387c"), "23fe38e8-72b8-4977-8d95-97cf7bd356f2", "Urgent", "Pending", "a0f27a1b-1880-4e65-9290-232cd0293764" },
                    { new Guid("f23b641c-6475-4c9c-8e84-db8553eebfc9"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e30bc099-8de0-4324-809b-51caedf0af2e"), "cbd3b811-10f4-4b29-954b-09d09716fb26", "Emergency", "Pending", "99aa8f8f-cee0-458e-a8a1-9dac2d9dfde1" }
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
