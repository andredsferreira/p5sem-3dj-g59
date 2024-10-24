using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DDDNetCore.Migrations.DDDSample1Db
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
                name: "OperationType",
                schema: "projeto5sem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anaesthesiaTime = table.Column<int>(type: "int", nullable: true),
                    surgeryTime = table.Column<int>(type: "int", nullable: true),
                    cleaningTime = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                    MedicalRecordNumber = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: true)
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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    staffRole = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
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
                    patientId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    staffId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    operationTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    priority = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requestStatus = table.Column<int>(type: "int", nullable: false)
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
                schema: "projeto5sem",
                table: "OperationType",
                columns: new[] { "Id", "Status", "anaesthesiaTime", "cleaningTime", "name", "surgeryTime" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/DDDSample1Db/20241023224054_Initial.cs
                    { new Guid("2b4d426f-e775-42b1-b053-691a91b74ba2"), "ACTIVE", null, null, "ACL Reconstruction", null },
                    { new Guid("612897d6-e155-4837-a8bc-a76a795a2e8c"), "ACTIVE", null, null, "Knee Replacement", null },
                    { new Guid("68626bb6-8507-4a70-9ecf-45cdbff3f1d6"), "ACTIVE", null, null, "Meniscal Injury Treatment", null },
                    { new Guid("9705d96e-b341-42ee-987d-180f33f8a5e1"), "ACTIVE", null, null, "Shoulder Replacement", null },
                    { new Guid("c94be47f-1d3d-4b61-91da-eef9a634419a"), "ACTIVE", null, null, "Hip Replacement", null }
========
                    { new Guid("1036ef74-1fc0-43fd-af25-d28e9b7d7e24"), "ACTIVE", null, null, "Knee Replacement", null },
                    { new Guid("32b8a4da-5242-403d-81df-03b6b79aea2d"), "ACTIVE", null, null, "Meniscal Injury Treatment", null },
                    { new Guid("55347851-39a1-4117-9b3d-ae0468e68abe"), "ACTIVE", null, null, "Shoulder Replacement", null },
                    { new Guid("978b1f44-6f15-4b40-9c6f-1235e8e50799"), "ACTIVE", null, null, "ACL Reconstruction", null },
                    { new Guid("b520c722-ebbb-4409-9852-f5cbe062eaa7"), "ACTIVE", null, null, "Hip Replacement", null }
>>>>>>>> fe98147 (Commit para cleaning):Migrations/DDDSample1Db/20241023145704_Initial.cs
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Patient",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName", "Gender", "MedicalRecordNumber", "PhoneNumber" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/DDDSample1Db/20241023224054_Initial.cs
                    { "27ec56a4-ebaf-43a3-96c1-4a0fab32175e", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", "202410000003", "910555333" },
                    { "67f5ac88-8db2-4900-9665-ccd01b14108d", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", "202410000002", "910555222" },
                    { "8b813cf5-a7ef-4b58-b437-00ebea78a0f6", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", "202410000001", "910555111" }
========
                    { "1cb2b22e-45c4-4686-8166-c801351c15fd", new DateOnly(2001, 10, 21), "patientA@hospital.com", "João Camião", "Male", null, "910555111" },
                    { "1f9495cd-b6ad-4995-804b-f638e5253057", new DateOnly(1995, 12, 30), "patientC@hospital.com", "Carla Ferreira", "Female", null, "910555333" },
                    { "803bbdd8-6b09-4b80-aef3-dc0397229a1c", new DateOnly(1998, 5, 14), "patientB@hospital.com", "Bruno Silva", "Male", null, "910555222" }
>>>>>>>> fe98147 (Commit para cleaning):Migrations/DDDSample1Db/20241023145704_Initial.cs
                });

            migrationBuilder.InsertData(
                schema: "projeto5sem",
                table: "Staff",
                columns: new[] { "Id", "staffRole" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/DDDSample1Db/20241023224054_Initial.cs
                    { new Guid("14626743-499d-4f69-bed5-0a234e0840ed"), "Doctor" },
                    { new Guid("14d255ba-7535-490e-abbf-c3eadee1b692"), "Doctor" },
                    { new Guid("6185d31b-39a4-4b01-8dd5-41e866fc048a"), "Nurse" },
                    { new Guid("6d7e46e0-878b-450b-8fac-a3039a352374"), "Nurse" },
                    { new Guid("94c80dfa-4c8c-4d21-bc29-47053fa17ac1"), "Admin" }
========
                    { new Guid("5c066848-2e40-4f96-9910-41e7ad69e524"), "Nurse" },
                    { new Guid("6d795d46-4c78-4521-97b3-56b4ec721c16"), "Nurse" },
                    { new Guid("7d12e017-f06b-4723-8541-7f0b043cd23e"), "Admin" },
                    { new Guid("dfa31c2f-9ca8-41ec-86d9-cf44f76be48b"), "Doctor" },
                    { new Guid("f9c03261-6822-4abc-9e04-400c19594486"), "Doctor" }
>>>>>>>> fe98147 (Commit para cleaning):Migrations/DDDSample1Db/20241023145704_Initial.cs
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_PatientId",
                table: "Allergy",
                column: "PatientId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "OperationRequest",
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
