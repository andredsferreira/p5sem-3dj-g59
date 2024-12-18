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
                    { "0c71dec1-f206-43b4-87c1-a045ea4d1633", null, "Patient", "PATIENT" },
                    { "571f2a9c-3aaf-4250-9a58-980506bed48c", null, "Nurse", "NURSE" },
                    { "61a3c51e-ab19-4294-ae45-4092c85e63eb", null, "Technician", "TECHNICIAN" },
                    { "bbf87d38-26e6-4705-a9be-bd484708283a", null, "Admin", "ADMIN" },
                    { "dfb01c05-b040-4d21-8a5f-82bff894aaf1", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1408c09d-ee0f-497d-86d9-ab00135e242c", 0, "eb6c0a11-6ada-40ce-9111-22e263fbf21c", "tiago@hospital.com", true, false, null, "TIAGO@HOSPITAL.COM", "TIAGO", "AQAAAAIAAYagAAAAEBeYw7kfZ/Jyiig5SDml5+TMLNwXCG3GKyEQPtqsNOpgFTK+nVOb3MJK9rYARdX9nQ==", null, false, "f980233a-3cf9-48eb-975c-05112a7ecfa6", false, "tiago" },
                    { "3042772e-3552-4d96-93e8-2dfc039898f5", 0, "f73573c5-9094-4f9f-bcff-3b89d55797a8", "andre@hospital.com", true, false, null, "ANDRE@HOSPITAL.COM", "ANDRE", "AQAAAAIAAYagAAAAEFKOo4/hwpua0QhOgJ80ZOh6qadlM4Ii+p9nL8oZ7pD5TJoqclays42ArGckzM7ijQ==", null, false, "1af89133-e355-46d5-a52d-df1b1e118d07", false, "andre" },
                    { "42c5c4b4-401f-48dc-ac9e-6d5a19d1ddc3", 0, "8f8b9e75-e999-40d4-bbb5-a03d6bf2528c", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEBN+ehjTtbihRVOd4BP66BZmq7tUZC61l3wl0BQzPHLpUmxT+e9B8/AKVaK1kiESnA==", null, false, "b832cff7-f657-45ce-bb45-a1e884d176cb", false, "technician" },
                    { "5cc33ae4-48e4-4290-9e24-5dada205d038", 0, "684b9f9a-8f38-41c5-9fca-112bd661a735", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEIRRbfIGSUkpeyyQ6QAIj55XEUj3cDgtVg83ZU9qxQGQEManAXt+EQ3z+tUz4SfleQ==", null, false, "51d753f0-274a-4194-8c8a-1e45d73fb95d", false, "patient" },
                    { "7d38bafb-20f4-4e1b-b883-f77fa975220c", 0, "3a8a277e-b370-454b-8582-f0f32048b8a1", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEMJhKt5AXa5v00m6m2vYDai2Bna2uqYS8vNjC9N1dCrHhJumsBbIYC8YBxp+EwHz1A==", null, false, "f1760ae6-a1dd-4ef0-bc9e-4022e4dfe7dd", false, "admin" },
                    { "860df055-835b-4ded-b14a-ec800201fbb0", 0, "458ca9b8-4df3-42c2-a034-bf7a0562dc0b", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEJPs4+2VZ/qc+4hHS4Gv2sbyIcvsphgHNQL6E/ZSQ0sz0PNioA+BJUe/7Fpl3E+1Rg==", null, false, "48e17e5a-23df-434e-8a92-bef811d0e6bd", false, "doctor" },
                    { "972ce454-319c-4cd5-bd84-fd73b971c7ff", 0, "6bfeb372-8e76-4a8c-b1c6-506d7cfea7d8", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAELMqygRmnCKf567FuBC2GsKb+cZradn6RXlsPQn69aG47R1gzxvbCMTtCw9T3E18wA==", null, false, "a034c520-801e-446a-bcd1-44b17783ba8c", false, "nurse" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "SpecializationId", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("016a502d-176d-44ea-a8c2-59489b7f331d"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 20, 15, "Lumbar disectomy", 45 },
                    { new Guid("4c283450-3c38-49d9-b8c8-2b80fee19db0"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "ACL reconstruction", 60 },
                    { new Guid("7e5f03bc-5a3a-43c9-8480-3ea69e70decb"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Trigger finger", 10 },
                    { new Guid("7f85666d-db84-4036-978e-38366ab9700f"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Knee replacement", 60 },
                    { new Guid("8a582fb4-bff6-4343-b8ae-0cc398b65487"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "Rotator cuff repair", 80 },
                    { new Guid("9470f06b-6d57-435e-9803-57d9d825a575"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Carpal tunnel syndrome", 10 },
                    { new Guid("b1328746-c4fe-48c5-85a1-4d21d973c38f"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 30, 20, "Ankle ligaments repair", 45 },
                    { new Guid("d7d8efcf-b217-4954-8a4f-ffb986f09dd4"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Shoulder replacement", 90 },
                    { new Guid("e7eb5e11-b0a2-4af7-87c1-086394e3c009"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 20, "Meniscal inury treatment", 45 },
                    { new Guid("e94e30e9-e0d8-4fb9-b77a-427f07b51ccd"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Hip replacement", 75 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "15f36b97-7a63-4cdc-91b6-2f03eaed8e81", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", "patientK@hospital.com" },
                    { "2f1d65d5-894c-43d7-8809-789aa8a5c7ac", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", "patientF@hospital.com" },
                    { "439dc01e-f8e9-49e9-bda8-93aac1470504", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", "patientB@hospital.com" },
                    { "4616a8ef-375f-46f2-9202-c717f191d4c8", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", "patientA@hospital.com" },
                    { "497dcd8c-e193-4cfd-9aa5-6815eac9a61e", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", "patientE@hospital.com" },
                    { "556eb046-e678-4bac-ba60-2fa1faf41431", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", "patientG@hospital.com" },
                    { "5d616ddb-d5da-4ac9-a06e-a9b2cc879aa7", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", "patientJ@hospital.com" },
                    { "5f5646e8-2683-4d0c-9559-5c7f11e50c09", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", "patientD@hospital.com" },
                    { "6b6f7434-98fe-4c9f-85a7-9ec1c049be6b", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", "patientM@hospital.com" },
                    { "7c088f64-3f05-417e-97b9-845f21c2e7cf", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", "patientC@hospital.com" },
                    { "8e68fe1c-d993-468d-81ad-6c66a2e7f8c3", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", "patientI@hospital.com" },
                    { "e931cfe4-f531-466b-a507-d14a209d70f0", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", "patientL@hospital.com" },
                    { "fde759b0-94ab-4b86-ad0d-7a19be0cc443", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", "patientH@hospital.com" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "0dc8ad22-d134-47db-8c0f-ca861fbd9e90", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "Nurse" },
                    { "5542cd6c-1b8b-4d32-ad10-c6332403c3b6", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "Doctor" },
                    { "9e46915f-bf8f-4675-8b05-789417c2d05c", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "Doctor" },
                    { "f47a78a5-097c-47c0-845c-73a386c3a2a1", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "Doctor" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "SurgeryRoom",
                columns: new[] { "Id", "AssignedEquipment", "Capacity", "MaintenanceSlots", "Number", "RoomStatus", "RoomType" },
                values: new object[,]
                {
                    { "010accec-6f8b-4236-a3e2-850e88974277", "[\"Scalpel\",\"Monitor\",\"Table\"]", 10, "[\"28/10/2024=[12:30,13:00];\"]", 201, "Available", "OperatingRoom" },
                    { "81bd757d-42b1-4ad0-8b05-0b316aecae77", "[\"Scalpel\",\"Monitor\"]", 10, "[\"28/10/2024=[09:30,10:00];\"]", 200, "Available", "OperatingRoom" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "dfb01c05-b040-4d21-8a5f-82bff894aaf1", "1408c09d-ee0f-497d-86d9-ab00135e242c" },
                    { "dfb01c05-b040-4d21-8a5f-82bff894aaf1", "3042772e-3552-4d96-93e8-2dfc039898f5" },
                    { "61a3c51e-ab19-4294-ae45-4092c85e63eb", "42c5c4b4-401f-48dc-ac9e-6d5a19d1ddc3" },
                    { "0c71dec1-f206-43b4-87c1-a045ea4d1633", "5cc33ae4-48e4-4290-9e24-5dada205d038" },
                    { "bbf87d38-26e6-4705-a9be-bd484708283a", "7d38bafb-20f4-4e1b-b883-f77fa975220c" },
                    { "dfb01c05-b040-4d21-8a5f-82bff894aaf1", "860df055-835b-4ded-b14a-ec800201fbb0" },
                    { "571f2a9c-3aaf-4250-9a58-980506bed48c", "972ce454-319c-4cd5-bd84-fd73b971c7ff" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("112a1f66-8e3c-4130-8741-c4686d0c9c4b"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c283450-3c38-49d9-b8c8-2b80fee19db0"), "15f36b97-7a63-4cdc-91b6-2f03eaed8e81", "Emergency", "Pending", "5542cd6c-1b8b-4d32-ad10-c6332403c3b6" },
                    { new Guid("13a035e0-0b8a-457d-874a-49e2f666ea95"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e94e30e9-e0d8-4fb9-b77a-427f07b51ccd"), "5f5646e8-2683-4d0c-9559-5c7f11e50c09", "Urgent", "Pending", "f47a78a5-097c-47c0-845c-73a386c3a2a1" },
                    { new Guid("1f1152b6-5a5e-408d-80c2-440995f8a29e"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b1328746-c4fe-48c5-85a1-4d21d973c38f"), "2f1d65d5-894c-43d7-8809-789aa8a5c7ac", "Elective", "Pending", "f47a78a5-097c-47c0-845c-73a386c3a2a1" },
                    { new Guid("2d23efb2-e6ad-499d-81bd-50b14873b4e7"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d7d8efcf-b217-4954-8a4f-ffb986f09dd4"), "7c088f64-3f05-417e-97b9-845f21c2e7cf", "Emergency", "Pending", "f47a78a5-097c-47c0-845c-73a386c3a2a1" },
                    { new Guid("2fcb8833-efc9-4887-8f82-3d0dd5274a83"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7f85666d-db84-4036-978e-38366ab9700f"), "e931cfe4-f531-466b-a507-d14a209d70f0", "Urgent", "Pending", "5542cd6c-1b8b-4d32-ad10-c6332403c3b6" },
                    { new Guid("431df31d-919a-4eec-9a11-884b42812ee8"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7eb5e11-b0a2-4af7-87c1-086394e3c009"), "497dcd8c-e193-4cfd-9aa5-6815eac9a61e", "Elective", "Pending", "9e46915f-bf8f-4675-8b05-789417c2d05c" },
                    { new Guid("507c9f46-a9e5-4bba-a560-e39031dd0dc4"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("b1328746-c4fe-48c5-85a1-4d21d973c38f"), "556eb046-e678-4bac-ba60-2fa1faf41431", "Emergency", "Pending", "9e46915f-bf8f-4675-8b05-789417c2d05c" },
                    { new Guid("53a15117-5b1d-4341-8b2c-bf9c0502ab0e"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7e5f03bc-5a3a-43c9-8480-3ea69e70decb"), "8e68fe1c-d993-468d-81ad-6c66a2e7f8c3", "Elective", "Pending", "5542cd6c-1b8b-4d32-ad10-c6332403c3b6" },
                    { new Guid("8203c087-e620-4a8d-9d49-c0502a751d84"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8a582fb4-bff6-4343-b8ae-0cc398b65487"), "2f1d65d5-894c-43d7-8809-789aa8a5c7ac", "Urgent", "Pending", "9e46915f-bf8f-4675-8b05-789417c2d05c" },
                    { new Guid("987688ef-8676-4e3d-a3af-a9578cfcb413"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9470f06b-6d57-435e-9803-57d9d825a575"), "5d616ddb-d5da-4ac9-a06e-a9b2cc879aa7", "Urgent", "Pending", "5542cd6c-1b8b-4d32-ad10-c6332403c3b6" },
                    { new Guid("b3895213-657b-4eb8-9cd1-9e0a5ccbd9ba"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e7eb5e11-b0a2-4af7-87c1-086394e3c009"), "6b6f7434-98fe-4c9f-85a7-9ec1c049be6b", "Emergency", "Pending", "f47a78a5-097c-47c0-845c-73a386c3a2a1" },
                    { new Guid("b742543f-6705-4340-a2c4-10188373d68b"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7f85666d-db84-4036-978e-38366ab9700f"), "439dc01e-f8e9-49e9-bda8-93aac1470504", "Urgent", "Pending", "f47a78a5-097c-47c0-845c-73a386c3a2a1" },
                    { new Guid("b9d4334b-52d4-4e4e-a207-eca9e4e28a61"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d7d8efcf-b217-4954-8a4f-ffb986f09dd4"), "6b6f7434-98fe-4c9f-85a7-9ec1c049be6b", "Urgent", "Pending", "5542cd6c-1b8b-4d32-ad10-c6332403c3b6" },
                    { new Guid("db8ac34e-5746-4a4c-ba72-a338e86dc44b"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c283450-3c38-49d9-b8c8-2b80fee19db0"), "4616a8ef-375f-46f2-9202-c717f191d4c8", "Elective", "Pending", "f47a78a5-097c-47c0-845c-73a386c3a2a1" },
                    { new Guid("edfcd32e-4592-45b0-b666-4d8396dc0d23"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e94e30e9-e0d8-4fb9-b77a-427f07b51ccd"), "4616a8ef-375f-46f2-9202-c717f191d4c8", "Emergency", "Pending", "5542cd6c-1b8b-4d32-ad10-c6332403c3b6" },
                    { new Guid("efe921cb-a158-4b27-a0aa-e9ae6c4a22cb"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7e5f03bc-5a3a-43c9-8480-3ea69e70decb"), "8e68fe1c-d993-468d-81ad-6c66a2e7f8c3", "Emergency", "Pending", "9e46915f-bf8f-4675-8b05-789417c2d05c" },
                    { new Guid("f4c9a2f0-c167-479a-a72b-f36a0f184256"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("016a502d-176d-44ea-a8c2-59489b7f331d"), "fde759b0-94ab-4b86-ad0d-7a19be0cc443", "Urgent", "Pending", "9e46915f-bf8f-4675-8b05-789417c2d05c" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Appointment",
                columns: new[] { "Id", "AppointmentStatus", "DateTime", "OperationRequestId", "SurgeryRoomId" },
                values: new object[,]
                {
                    { new Guid("389d068a-3205-4105-af2b-f5c3062161c1"), "Scheduled", "28/10/2024 18:30:00", new Guid("b742543f-6705-4340-a2c4-10188373d68b"), "010accec-6f8b-4236-a3e2-850e88974277" },
                    { new Guid("4d2c0d90-05fa-4b14-87c8-4a87c91fd0f7"), "Scheduled", "28/10/2024 10:30:00", new Guid("db8ac34e-5746-4a4c-ba72-a338e86dc44b"), "81bd757d-42b1-4ad0-8b05-0b316aecae77" }
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
