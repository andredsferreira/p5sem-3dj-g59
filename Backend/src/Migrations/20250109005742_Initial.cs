using System;
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
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "Specialization", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("001c35e0-c535-44ca-8dde-10dfcb30b9e4"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 20, 15, "Lumbar disectomy", 45 },
                    { new Guid("07a7607e-7928-4b87-a7db-f47672e05e47"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 30, "Rotator cuff repair", 80 },
                    { new Guid("3953d992-aa3a-482a-8da0-b7364776a783"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 20, "Meniscal inury treatment", 45 },
                    { new Guid("490bd87e-09a9-47a6-93c0-3cc23c040487"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 30, "ACL reconstruction", 60 },
                    { new Guid("4c69349b-7bee-44d3-bcf3-7e52f551cbf0"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 30, 20, "Ankle ligaments repair", 45 },
                    { new Guid("4d4a71f2-22dd-4216-8422-268a8fb0f161"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 15, 15, "Carpal tunnel syndrome", 10 },
                    { new Guid("7568d7e7-c051-4fe0-9406-05eafc4d26ea"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 45, "Shoulder replacement", 90 },
                    { new Guid("bbb20be3-add1-4d3f-9a03-cc1f15ae7c2a"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 15, 15, "Trigger finger", 10 },
                    { new Guid("c53367c3-88fc-4c7f-aad2-9d55add5f560"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 45, "Knee replacement", 60 },
                    { new Guid("d318e3d0-cea0-4f9d-80b6-759628435998"), 0, 0, 0, 0, 0, 0, 0, 0, "ACTIVE", 45, 45, "Hip replacement", 75 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "05b33510-bbb7-4593-95f2-3c8c511d09f2", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", "patientK@hospital.com" },
                    { "3b0a2db5-97fa-4db0-b05c-5b58471d5965", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", "patientI@hospital.com" },
                    { "57e171b5-e294-4512-b272-0ac0cf826ad2", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", "patientD@hospital.com" },
                    { "72f73b1c-74a6-4405-bb7b-93bf50f0cf6f", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", "patientF@hospital.com" },
                    { "7dd3d356-aaaf-4fa2-91a5-ab612f9b9512", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", "patientA@hospital.com" },
                    { "846d0d23-fd94-4fdb-9903-a13ee6629946", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", "patientM@hospital.com" },
                    { "bde37e08-62ed-4213-9ea5-42f4e9a392b3", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", "patientJ@hospital.com" },
                    { "ccff01c4-504c-4843-90a1-f1f672264305", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", "patientE@hospital.com" },
                    { "d47df3e1-c48d-4090-a083-793a4654ceea", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", "patientH@hospital.com" },
                    { "dcaddc0c-c95c-4746-ac97-4e04c3f0862f", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", "patientC@hospital.com" },
                    { "df97308b-74fd-4461-accd-c6daaf70a410", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", "patientB@hospital.com" },
                    { "e24567a6-07af-4a02-ab62-08d1bf8d20bb", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", "patientG@hospital.com" },
                    { "e56adecb-ec01-4b7b-a601-ed038bc9fcc1", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", "patientL@hospital.com" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "407308ea-ea2c-417a-b043-2c84bc778ee6", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "nurse" },
                    { "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "doctor" },
                    { "951d83dd-0e0e-4d3e-9c3d-ae972e71e682", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "doctor" },
                    { "dce5b8c2-29c0-4016-9bef-984f247f98e0", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "doctor" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "SurgeryRoom",
                columns: new[] { "Id", "AssignedEquipment", "Capacity", "MaintenanceSlots", "Number", "RoomStatus", "RoomType" },
                values: new object[,]
                {
                    { "128b6c00-2298-4283-a22e-1dd306128464", "[\"Scalpel\",\"Monitor\",\"Table\"]", 10, "[\"28/10/2024=[12:30,13:00];\"]", 201, "Available", "OperatingRoom" },
                    { "494736ae-6226-4bf3-80a4-a9d16eb3088d", "[\"Scalpel\",\"Monitor\"]", 10, "[\"28/10/2024=[09:30,10:00];\"]", 200, "Available", "OperatingRoom" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("0ece32b5-8c27-47dd-a427-ea0bde0eeb53"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c69349b-7bee-44d3-bcf3-7e52f551cbf0"), "72f73b1c-74a6-4405-bb7b-93bf50f0cf6f", "Elective", "Pending", "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726" },
                    { new Guid("1f697604-31fc-4067-a6a8-0d8da48eb410"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3953d992-aa3a-482a-8da0-b7364776a783"), "ccff01c4-504c-4843-90a1-f1f672264305", "Elective", "Pending", "951d83dd-0e0e-4d3e-9c3d-ae972e71e682" },
                    { new Guid("282c1bd5-64e0-4872-8b1b-4f21bd7b0c8b"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("490bd87e-09a9-47a6-93c0-3cc23c040487"), "05b33510-bbb7-4593-95f2-3c8c511d09f2", "Emergency", "Pending", "dce5b8c2-29c0-4016-9bef-984f247f98e0" },
                    { new Guid("424d80bf-8453-4735-bab5-1d704d5ae3f3"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbb20be3-add1-4d3f-9a03-cc1f15ae7c2a"), "3b0a2db5-97fa-4db0-b05c-5b58471d5965", "Emergency", "Pending", "951d83dd-0e0e-4d3e-9c3d-ae972e71e682" },
                    { new Guid("45ab6785-77a3-4706-a00a-c098002cada7"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7568d7e7-c051-4fe0-9406-05eafc4d26ea"), "dcaddc0c-c95c-4746-ac97-4e04c3f0862f", "Emergency", "Pending", "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726" },
                    { new Guid("4ddc3c2c-4e5b-4a98-bd11-834746871f4d"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d318e3d0-cea0-4f9d-80b6-759628435998"), "57e171b5-e294-4512-b272-0ac0cf826ad2", "Urgent", "Pending", "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726" },
                    { new Guid("515148e4-a70d-4f08-85d3-4891f1b51297"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d318e3d0-cea0-4f9d-80b6-759628435998"), "7dd3d356-aaaf-4fa2-91a5-ab612f9b9512", "Emergency", "Pending", "dce5b8c2-29c0-4016-9bef-984f247f98e0" },
                    { new Guid("662f2476-1725-4989-af2f-7bdc7c263e38"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c53367c3-88fc-4c7f-aad2-9d55add5f560"), "e56adecb-ec01-4b7b-a601-ed038bc9fcc1", "Urgent", "Pending", "dce5b8c2-29c0-4016-9bef-984f247f98e0" },
                    { new Guid("7119fc3a-8bb3-42c8-a594-8e80c90690ff"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("07a7607e-7928-4b87-a7db-f47672e05e47"), "72f73b1c-74a6-4405-bb7b-93bf50f0cf6f", "Urgent", "Pending", "951d83dd-0e0e-4d3e-9c3d-ae972e71e682" },
                    { new Guid("73cd2013-c1df-4a7d-9377-476303f3bfb2"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c53367c3-88fc-4c7f-aad2-9d55add5f560"), "df97308b-74fd-4461-accd-c6daaf70a410", "Urgent", "Pending", "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726" },
                    { new Guid("8015bf49-39ff-4b8f-856a-5ce3b55d235e"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bbb20be3-add1-4d3f-9a03-cc1f15ae7c2a"), "3b0a2db5-97fa-4db0-b05c-5b58471d5965", "Elective", "Pending", "dce5b8c2-29c0-4016-9bef-984f247f98e0" },
                    { new Guid("8099d9d9-d83a-4a5a-a880-c7048a19fb0f"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("001c35e0-c535-44ca-8dde-10dfcb30b9e4"), "d47df3e1-c48d-4090-a083-793a4654ceea", "Urgent", "Pending", "951d83dd-0e0e-4d3e-9c3d-ae972e71e682" },
                    { new Guid("8c1180ee-fda2-4efd-b1f1-5f353c70279c"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7568d7e7-c051-4fe0-9406-05eafc4d26ea"), "846d0d23-fd94-4fdb-9903-a13ee6629946", "Urgent", "Pending", "dce5b8c2-29c0-4016-9bef-984f247f98e0" },
                    { new Guid("a9697fb1-dc2e-4209-8d6d-cc56a0310dc4"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3953d992-aa3a-482a-8da0-b7364776a783"), "846d0d23-fd94-4fdb-9903-a13ee6629946", "Emergency", "Pending", "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726" },
                    { new Guid("aa513694-e6e1-407a-b7d0-026ab710a200"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4d4a71f2-22dd-4216-8422-268a8fb0f161"), "bde37e08-62ed-4213-9ea5-42f4e9a392b3", "Urgent", "Pending", "dce5b8c2-29c0-4016-9bef-984f247f98e0" },
                    { new Guid("ab075cde-7feb-4532-a04f-53c3721bb280"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("490bd87e-09a9-47a6-93c0-3cc23c040487"), "7dd3d356-aaaf-4fa2-91a5-ab612f9b9512", "Elective", "Pending", "50dfbfee-e1ce-4fe2-a93b-4e54d8d7d726" },
                    { new Guid("bc226115-d0c4-4e95-b77e-3797664ef870"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("4c69349b-7bee-44d3-bcf3-7e52f551cbf0"), "e24567a6-07af-4a02-ab62-08d1bf8d20bb", "Emergency", "Pending", "951d83dd-0e0e-4d3e-9c3d-ae972e71e682" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Appointment",
                columns: new[] { "Id", "AppointmentStatus", "DateTime", "OperationRequestId", "SurgeryRoomId" },
                values: new object[,]
                {
                    { new Guid("33e4913e-f311-4d70-9239-43c0473e7a91"), "Scheduled", "28/10/2024 10:30:00", new Guid("ab075cde-7feb-4532-a04f-53c3721bb280"), "494736ae-6226-4bf3-80a4-a9d16eb3088d" },
                    { new Guid("dfe809ce-c9a4-4979-ac3b-d57d33939d5d"), "Scheduled", "28/10/2024 18:30:00", new Guid("73cd2013-c1df-4a7d-9377-476303f3bfb2"), "128b6c00-2298-4283-a22e-1dd306128464" }
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
                name: "DomainLog",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "OperationRequest",
                schema: "projeto5sem");

            migrationBuilder.DropTable(
                name: "SurgeryRoom",
                schema: "projeto5sem");

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
