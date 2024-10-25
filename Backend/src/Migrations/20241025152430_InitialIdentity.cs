using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241025140740_InitialIdentity.cs
                    { "1d555ac9-6e33-40c3-aa56-47aaba3c5a5d", null, "Doctor", "DOCTOR" },
                    { "58b08a57-d5e1-4500-b90b-52102d569f6c", null, "Technician", "TECHNICIAN" },
                    { "58c0520f-ebe5-4ee4-84a4-f0ad3eb64f74", null, "Nurse", "NURSE" },
                    { "9027ccb2-7156-4803-bc3d-c5d61173a329", null, "Admin", "ADMIN" },
                    { "bd85fe3b-1cd9-43df-8fc6-4ff27a21413b", null, "Patient", "PATIENT" }
========
                    { "014cf275-0880-41e2-822c-b39980346e36", null, "Technician", "TECHNICIAN" },
                    { "1d7be275-9dff-4eb4-bba2-a750ee357a3e", null, "Nurse", "NURSE" },
                    { "392dffbf-c3c4-4333-bfc1-42fee64386b3", null, "Admin", "ADMIN" },
                    { "91adb9ce-ef24-46a2-b5e7-40c40d3225a8", null, "Patient", "PATIENT" },
                    { "b7ef8084-4795-41f2-be6f-6150f8d66c76", null, "Doctor", "DOCTOR" }
>>>>>>>> eb8668e (Correction of bugs in services, repositories, and dependency injection.):Backend/src/Migrations/20241025152430_InitialIdentity.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241025140740_InitialIdentity.cs
                    { "571646a2-b9ca-445d-b2d4-1b1df907e32c", 0, "388b065a-8747-48c9-b387-6bf8091c5d74", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEPjSY4rJzNOtHXaQA+XWZnLHiBh2WEuq0IfP/13u7AiK9ZuO+wFESiL3EqAggjKflw==", null, false, "a3ac9132-cdb9-4bbe-aa5a-69fabf552fd8", false, "technician" },
                    { "a2bb422f-4f30-4c4c-8e87-893d1ccd6b56", 0, "3172a58c-23b8-4392-9844-60f2b79f5546", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEDxtSa02790qSG1gpzk4pLBDCU5NkUbqp1lOPqueaa72eP2n6x4e87du/Lz16xVHQg==", null, false, "d0443327-af01-4672-8776-dfb1d7423c20", false, "nurse" },
                    { "f1039d04-82b8-4be0-948a-410733b0cf10", 0, "2c60a603-24dc-4a28-90f9-baaa1024d176", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEJnXR3xno8tDIdvWP4Foef2Z8MfsubMM60bLIy6TpjezIusQGrkrMqHmnlI6WAfcNg==", null, false, "f84d1155-7bc8-4a7d-8e0c-31f1f2de0bb0", false, "doctor" },
                    { "faf660c3-4727-4204-9739-931ea5d14fd7", 0, "8db9c441-94b5-4dbf-87e0-80dbd75b0915", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAENEBrcOr4BR3TlwT+H7kkgPOtx3gBYkzuVCiDpkR2bLxHA7Ie7wJzpunNWOFsK6Fqg==", null, false, "f486b8ce-ddef-4999-a7bf-0a008d121139", false, "patient" },
                    { "fe5d35cf-0f76-4a9c-aaf8-c1323d76c1b8", 0, "c739777d-bf6d-48be-b81e-305257986cf7", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEM/WcKpeu2jVmGTqhdK1tB1fMOa/9v+Lci5WaelJ5/Z68XfYnxCh69Gq3nf+/s7ulA==", null, false, "192144a8-efa1-48d0-8cd2-d32365742465", false, "admin" }
========
                    { "0080e433-bb95-44b5-a042-6b124e895094", 0, "3654f272-2ec1-4eda-9aab-0b98fa6eb13e", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEPuYWTik8wVGlYgoNbzhuaHM7tgvzozsKMbOBfhG9niEntq/8XchA0UAkWiNBEbspA==", null, false, "82dac689-60a5-4eed-a0ec-7a81374a41e2", false, "admin" },
                    { "4a0f0e00-9550-44a8-bc17-c998c1731e78", 0, "38a412c8-91f3-45a0-902e-182c6a853654", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEHQ6eFFGOUsTwn+WG6QiprexBONFagmRgSlvsW9itZ2veJEGCGc5Z7uwdwYly/S87A==", null, false, "bdf269a7-4103-455c-9575-0ba04c7bde56", false, "doctor" },
                    { "5e489457-486c-47ac-8a8d-0fa61b8f404a", 0, "75fa8951-64c5-402b-9063-030e7da7a983", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEJ34iIxxSS2sMej0wkUtOPt6o63Yo65QfIB4+foKeBtIMyaPX/KSfPGZkQl+4rVndg==", null, false, "82e7a04b-a03a-4cf7-a88d-16d5b8109948", false, "nurse" },
                    { "974afc68-cd60-407c-8609-4b6d549422f0", 0, "efc1c9fc-d23c-4746-ab6c-1205988a7568", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEF31ZBP7100UT+EjenllwmshlilRfEpFTGTZORM8eMJ/3OfJKddGOewFMdkTFgHmOg==", null, false, "7d6ccb5b-afe0-45e5-9987-dc5c88360089", false, "patient" },
                    { "e2ee989a-c854-4b36-ac89-06ebd641ffed", 0, "4c1bd053-d303-474d-90a3-3ba3a0933779", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEGck6PkltZLWEq6ByOnFGoRKZJ1vMP3yrCLNUk35tyUyFnOnVFHB6X6AnMSb6OAYJg==", null, false, "5da6cf7f-c6ee-4e52-a944-ef7c01de656f", false, "technician" }
>>>>>>>> eb8668e (Correction of bugs in services, repositories, and dependency injection.):Backend/src/Migrations/20241025152430_InitialIdentity.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241025140740_InitialIdentity.cs
                    { "58b08a57-d5e1-4500-b90b-52102d569f6c", "571646a2-b9ca-445d-b2d4-1b1df907e32c" },
                    { "58c0520f-ebe5-4ee4-84a4-f0ad3eb64f74", "a2bb422f-4f30-4c4c-8e87-893d1ccd6b56" },
                    { "1d555ac9-6e33-40c3-aa56-47aaba3c5a5d", "f1039d04-82b8-4be0-948a-410733b0cf10" },
                    { "bd85fe3b-1cd9-43df-8fc6-4ff27a21413b", "faf660c3-4727-4204-9739-931ea5d14fd7" },
                    { "9027ccb2-7156-4803-bc3d-c5d61173a329", "fe5d35cf-0f76-4a9c-aaf8-c1323d76c1b8" }
========
                    { "392dffbf-c3c4-4333-bfc1-42fee64386b3", "0080e433-bb95-44b5-a042-6b124e895094" },
                    { "b7ef8084-4795-41f2-be6f-6150f8d66c76", "4a0f0e00-9550-44a8-bc17-c998c1731e78" },
                    { "1d7be275-9dff-4eb4-bba2-a750ee357a3e", "5e489457-486c-47ac-8a8d-0fa61b8f404a" },
                    { "91adb9ce-ef24-46a2-b5e7-40c40d3225a8", "974afc68-cd60-407c-8609-4b6d549422f0" },
                    { "014cf275-0880-41e2-822c-b39980346e36", "e2ee989a-c854-4b36-ac89-06ebd641ffed" }
>>>>>>>> eb8668e (Correction of bugs in services, repositories, and dependency injection.):Backend/src/Migrations/20241025152430_InitialIdentity.cs
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
