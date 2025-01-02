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
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "MinAnesthetist", "MinCirculatingNurse", "MinDoctor", "MinInstrumentingNurse", "MinMedicalActionAssistant", "MinNurseAnaesthetist", "MinXRayTechnician", "SpecializationId", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
                    { new Guid("00ef1f89-1dd4-4c24-b1cb-1039a5c72d31"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Hip replacement", 75 },
                    { new Guid("19cf8f06-7506-4751-b0db-1682378ffe1b"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "Rotator cuff repair", 80 },
                    { new Guid("47db2f8f-adff-4257-9df4-3a8debfcca10"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Trigger finger", 10 },
                    { new Guid("547aefa3-7b4d-4b46-ac7b-6a9f59e01583"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 20, "Meniscal inury treatment", 45 },
                    { new Guid("8dd68b42-a1ab-47b3-9922-a7d710d47158"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Shoulder replacement", 90 },
                    { new Guid("aece0fc2-d4c6-4032-b2a8-e57c7f199ab7"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 45, "Knee replacement", 60 },
                    { new Guid("be4fb15b-0fd5-4bec-adf6-0e9501f23637"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 30, 20, "Ankle ligaments repair", 45 },
                    { new Guid("d7ea178a-0db9-435b-bbe9-c679fdb518e8"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 15, 15, "Carpal tunnel syndrome", 10 },
                    { new Guid("e1c1f276-724d-4f34-8c3b-4de560fc346b"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 20, 15, "Lumbar disectomy", 45 },
                    { new Guid("f0341388-9655-465e-ab81-879b176e3c66"), 0, 0, 0, 0, 0, 0, 0, null, "ACTIVE", 45, 30, "ACL reconstruction", 60 }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber", "UserEmail" },
                values: new object[,]
                {
                    { "0b418a6e-10ac-473e-a68b-3e06a2f21d38", new DateOnly(1993, 12, 5), "patientM@hospital.com", "Marta Silva", "Female", "202410000013", "910555343", "patientM@hospital.com" },
                    { "40ab9b22-0d93-457b-b8b9-50e4b55651a6", new DateOnly(1994, 1, 15), "patientI@hospital.com", "Isabel Pereira", "Female", "202410000009", "910555339", "patientI@hospital.com" },
                    { "4c6eab25-6148-4e42-b192-6fe29355c401", new DateOnly(2000, 11, 2), "patientG@hospital.com", "Gabriela Santos", "Female", "202410000007", "910555337", "patientG@hospital.com" },
                    { "50e865c8-39cf-4b59-a50c-236a27dffd70", new DateOnly(1990, 7, 19), "patientH@hospital.com", "Henrique Almeida", "Male", "202410000008", "910555338", "patientH@hospital.com" },
                    { "6136c455-52f4-4f90-921c-5c2cb98c28ed", new DateOnly(1988, 5, 14), "patientD@hospital.com", "David Oliveira", "Male", "202410000004", "910555334", "patientD@hospital.com" },
                    { "616c9166-395e-428a-ac69-7c3c35e3fd7b", new DateOnly(1996, 9, 25), "patientK@hospital.com", "Karina Martins", "Female", "202410000011", "910555341", "patientK@hospital.com" },
                    { "68aff3df-5eb4-4c9b-8b9b-21d40994b9c3", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111", "patientA@hospital.com" },
                    { "791c4458-b797-4557-a9fc-44f4a9607a57", new DateOnly(1987, 4, 12), "patientL@hospital.com", "Lucas Rodrigues", "Male", "202410000012", "910555342", "patientL@hospital.com" },
                    { "890fc03a-5365-4541-aca8-02cd4a5b48c2", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222", "patientB@hospital.com" },
                    { "d3a159c6-d82e-4862-bc11-536201f5ee2d", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333", "patientC@hospital.com" },
                    { "d45acb35-43c8-4f57-818d-b4f3d17e0bdc", new DateOnly(1992, 8, 22), "patientE@hospital.com", "Emma Sousa", "Female", "202410000005", "910555335", "patientE@hospital.com" },
                    { "db93a935-d3a1-458a-8c9e-98e44e76633a", new DateOnly(1985, 3, 10), "patientF@hospital.com", "Felipe Costa", "Male", "202410000006", "910555336", "patientF@hospital.com" },
                    { "efe8c432-d3db-4cb5-8611-19e70128d018", new DateOnly(1982, 6, 8), "patientJ@hospital.com", "João Lima", "Male", "202410000010", "910555340", "patientJ@hospital.com" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Specialization",
                columns: new[] { "Id", "codeSpec", "description", "designation" },
                values: new object[,]
                {
                    { new Guid("2ba12613-98de-40f4-bdc0-40c5e1d24209"), "PRO", "Prosthetics", "Prosthethiscs" },
                    { new Guid("7172b437-5a79-4953-9b51-180a0452ee47"), "SPN", "Spine", "Spine" },
                    { new Guid("c969e14e-ad11-4c3a-88fc-b90351e74cef"), "ART", "Arthroscopy", "Arthroscopy" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "Email", "FullName", "IdentityUsername", "LicenseNumber", "PhoneNumber", "StaffRole" },
                values: new object[,]
                {
                    { "5ffb9ab8-694d-448f-90b5-d2cb2d561eab", "andre@hospital.com", "André de Sousa Ferreira", "andre", "f47ac10b-58cc-4372-a567-0e02b2c3d479", "920555222", "doctor" },
                    { "9618f55d-45fb-458a-88e5-e74f854b5491", "tiago@hospital.com", "Tiago Filipe Carvalho Nunes", "tiago", "f57ac10b-68cc-5372-a567-1e02b2c3d479", "930555333", "doctor" },
                    { "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc", "pedro@hospital.com", "Pedro Carvalho Oliveira Monteiro", "pedro", "f47ac10b-08cc-4372-a507-0e02b2d3d479", "910555111", "doctor" },
                    { "cb31ca5f-0452-4c12-9803-7788f1a01355", "nurse@hospital.com", "Nurse One", "nurse", "n47ac10b-58cc-4372-a567-0e02b2c3d481", "910555567", "nurse" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "SurgeryRoom",
                columns: new[] { "Id", "AssignedEquipment", "Capacity", "MaintenanceSlots", "Number", "RoomStatus", "RoomType" },
                values: new object[,]
                {
                    { "838b2a4c-bc7b-433e-9cd9-c948c2304510", "[\"Scalpel\",\"Monitor\",\"Table\"]", 10, "[\"28/10/2024=[12:30,13:00];\"]", 201, "Available", "OperatingRoom" },
                    { "cff0b0a9-ef67-449f-a8cd-dbe352fc72b3", "[\"Scalpel\",\"Monitor\"]", 10, "[\"28/10/2024=[09:30,10:00];\"]", 200, "Available", "OperatingRoom" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "OperationRequest",
                columns: new[] { "Id", "dateTime", "operationTypeId", "patientId", "priority", "requestStatus", "staffId" },
                values: new object[,]
                {
                    { new Guid("08f42fef-d35b-4669-92b0-96471413415c"), new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8dd68b42-a1ab-47b3-9922-a7d710d47158"), "0b418a6e-10ac-473e-a68b-3e06a2f21d38", "Urgent", "Pending", "9618f55d-45fb-458a-88e5-e74f854b5491" },
                    { new Guid("19c03335-f7bd-405c-a409-33d60b2169f1"), new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e1c1f276-724d-4f34-8c3b-4de560fc346b"), "50e865c8-39cf-4b59-a50c-236a27dffd70", "Urgent", "Pending", "5ffb9ab8-694d-448f-90b5-d2cb2d561eab" },
                    { new Guid("1a944ac7-1fcc-42cf-9c1f-d6bff3de8ff7"), new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("47db2f8f-adff-4257-9df4-3a8debfcca10"), "40ab9b22-0d93-457b-b8b9-50e4b55651a6", "Emergency", "Pending", "5ffb9ab8-694d-448f-90b5-d2cb2d561eab" },
                    { new Guid("2fdc86c8-120a-4bb0-958f-a1694f77f510"), new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("be4fb15b-0fd5-4bec-adf6-0e9501f23637"), "4c6eab25-6148-4e42-b192-6fe29355c401", "Emergency", "Pending", "5ffb9ab8-694d-448f-90b5-d2cb2d561eab" },
                    { new Guid("48705119-475d-4d2a-9d6d-bb2f88de0820"), new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aece0fc2-d4c6-4032-b2a8-e57c7f199ab7"), "791c4458-b797-4557-a9fc-44f4a9607a57", "Urgent", "Pending", "9618f55d-45fb-458a-88e5-e74f854b5491" },
                    { new Guid("48dd1dd2-e7ff-435c-b943-84e9eba68dfc"), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aece0fc2-d4c6-4032-b2a8-e57c7f199ab7"), "890fc03a-5365-4541-aca8-02cd4a5b48c2", "Urgent", "Pending", "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc" },
                    { new Guid("49f5ac2c-2dc0-47f4-bd73-68c2737586ec"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("19cf8f06-7506-4751-b0db-1682378ffe1b"), "db93a935-d3a1-458a-8c9e-98e44e76633a", "Urgent", "Pending", "5ffb9ab8-694d-448f-90b5-d2cb2d561eab" },
                    { new Guid("4f7668dc-c233-407e-8531-e88e47a233ce"), new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d7ea178a-0db9-435b-bbe9-c679fdb518e8"), "efe8c432-d3db-4cb5-8611-19e70128d018", "Urgent", "Pending", "9618f55d-45fb-458a-88e5-e74f854b5491" },
                    { new Guid("5dc1d731-fd8e-4085-88a5-46cbc088d65f"), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("547aefa3-7b4d-4b46-ac7b-6a9f59e01583"), "d45acb35-43c8-4f57-818d-b4f3d17e0bdc", "Elective", "Pending", "5ffb9ab8-694d-448f-90b5-d2cb2d561eab" },
                    { new Guid("5fabaa99-0080-4d16-ae36-e7b5d819daf9"), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("be4fb15b-0fd5-4bec-adf6-0e9501f23637"), "db93a935-d3a1-458a-8c9e-98e44e76633a", "Elective", "Pending", "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc" },
                    { new Guid("6765e71e-a465-4e9a-85c5-099c7f6cde26"), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("547aefa3-7b4d-4b46-ac7b-6a9f59e01583"), "0b418a6e-10ac-473e-a68b-3e06a2f21d38", "Emergency", "Pending", "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc" },
                    { new Guid("a577f9ab-79cf-48b6-8399-3a5805be42bc"), new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00ef1f89-1dd4-4c24-b1cb-1039a5c72d31"), "68aff3df-5eb4-4c9b-8b9b-21d40994b9c3", "Emergency", "Pending", "9618f55d-45fb-458a-88e5-e74f854b5491" },
                    { new Guid("a832894e-c8d0-43d1-9b12-f61d881ecd38"), new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0341388-9655-465e-ab81-879b176e3c66"), "616c9166-395e-428a-ac69-7c3c35e3fd7b", "Emergency", "Pending", "9618f55d-45fb-458a-88e5-e74f854b5491" },
                    { new Guid("cae569af-afe9-404d-9da1-76b84eecce7f"), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f0341388-9655-465e-ab81-879b176e3c66"), "68aff3df-5eb4-4c9b-8b9b-21d40994b9c3", "Elective", "Pending", "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc" },
                    { new Guid("e756311d-236f-4bd4-992d-cea4ee623db8"), new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00ef1f89-1dd4-4c24-b1cb-1039a5c72d31"), "6136c455-52f4-4f90-921c-5c2cb98c28ed", "Urgent", "Pending", "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc" },
                    { new Guid("eb9c4c9b-dbf7-4080-855b-d47775a144ab"), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("47db2f8f-adff-4257-9df4-3a8debfcca10"), "40ab9b22-0d93-457b-b8b9-50e4b55651a6", "Elective", "Pending", "9618f55d-45fb-458a-88e5-e74f854b5491" },
                    { new Guid("ef53d7c2-00cf-4ef5-b311-09cd76222339"), new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("8dd68b42-a1ab-47b3-9922-a7d710d47158"), "d3a159c6-d82e-4862-bc11-536201f5ee2d", "Emergency", "Pending", "9f2e2449-b161-43d8-bf7b-4a6f88fd45bc" }
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Appointment",
                columns: new[] { "Id", "AppointmentStatus", "DateTime", "OperationRequestId", "SurgeryRoomId" },
                values: new object[,]
                {
                    { new Guid("2649aee7-7d75-428d-909a-4463c4d23cc7"), "Scheduled", "28/10/2024 10:30:00", new Guid("cae569af-afe9-404d-9da1-76b84eecce7f"), "cff0b0a9-ef67-449f-a8cd-dbe352fc72b3" },
                    { new Guid("311ec4fa-cae9-45e1-8b5f-a1ae24ae6019"), "Scheduled", "28/10/2024 18:30:00", new Guid("48dd1dd2-e7ff-435c-b943-84e9eba68dfc"), "838b2a4c-bc7b-433e-9cd9-c948c2304510" }
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

            migrationBuilder.DropTable(
                name: "Specialization",
                schema: "projeto5sem");
        }
    }
}
