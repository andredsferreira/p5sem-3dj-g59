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
<<<<<<<< HEAD:Backend/src/Migrations/20241023224044_InitialIdentity.cs
                    { "43aaea9f-bf31-4c1c-82e0-30ab390acd4f", null, "Technician", "TECHNICIAN" },
                    { "6bc4e754-5d0e-49f1-a1ae-4b8917be8461", null, "Nurse", "NURSE" },
                    { "b04d8fd4-a52c-4f6d-a14d-401af7342c94", null, "Doctor", "DOCTOR" },
                    { "b12fd2fb-878c-4f65-8d1e-ee4a04c6cea2", null, "Patient", "PATIENT" },
                    { "fd494d48-a044-4191-9131-d14b4f25ff42", null, "Admin", "ADMIN" }
========
                    { "2ec62ad5-96a8-4929-9514-58ff2ecd9cba", null, "Patient", "PATIENT" },
                    { "531e44b0-e9a0-42de-9546-19864bee5624", null, "Admin", "ADMIN" },
                    { "99be996b-5516-48ef-a70a-e3f49c0304c9", null, "Nurse", "NURSE" },
                    { "b9fc8d2c-f4ca-46bc-9803-46a891ec31ac", null, "Technician", "TECHNICIAN" },
                    { "ca6eb8e2-4d51-4824-8fac-05b98adc067b", null, "Doctor", "DOCTOR" }
>>>>>>>> fe98147 (Commit para cleaning):Migrations/20241023145657_InitialIdentity.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241023224044_InitialIdentity.cs
                    { "633173eb-d633-4456-a98b-8f0dbbc95606", 0, "65c25a1c-91ab-4c72-b685-01760efd7c70", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEK9vMkyHphfmzgoQHcfXmb2yIoMrS1Au0o1YCgDhOuy12z/yyObIIwJIEOMa2sjCig==", null, false, "d7506cd4-1e7a-482d-84b8-e292d0e36d44", false, "admin" },
                    { "70b77d4c-86a7-4f46-b678-bde105ecb63d", 0, "3eba6698-323c-4831-a140-86e5b11faceb", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAENOyk/amHViHpT9fR1R4YB7tD6+pnNe3KmvUyq2FqRo50vnTnF+IE9bCY3k7EagnJg==", null, false, "bb564b9a-5b04-42b1-88bf-8a4b9bdd2970", false, "patient" },
                    { "78b8acfd-8c8f-41b5-9d30-3cb3b936268c", 0, "f317e35d-7f9d-422d-995e-593269a16a8d", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEGY6odnUsF3zzVJb1FqIe0oU3EbriR1cC65yxqdBOwamCA99zqd4hTLR3TWRikLrvw==", null, false, "5b377c8d-7914-4e03-be92-397a12ae93b2", false, "nurse" },
                    { "93bd1d2d-aa93-4def-8ad5-774a92c58ec1", 0, "d091cec1-6afc-4329-a648-05e97cafa61b", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEOMPREAx5fWb4XOIYC89PpQt3qfGWGzumrVzFVFWPHcF8KMJQ3R8thA0yytl0ag9Kg==", null, false, "bb4e0b83-109a-40a6-9abb-94527d6fb773", false, "technician" },
                    { "93f9b31e-0f8c-49a6-9832-2b931dec97bc", 0, "320808aa-1dd0-4cd0-9a09-ef6561787eac", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEBC2oro1zCHAlgedAEGBBV0LlejKye+d1R9hFTTI0zm+cg4xgNf6gb+JPudxs76Hlw==", null, false, "2a8a0a5c-b8f9-4b11-93d8-bea19e5ee030", false, "doctor" }
========
                    { "340e4ab6-9d69-41d2-826a-c8f3f2eabf2c", 0, "9f1b0b9a-641c-489d-a3a5-e09d36b94b10", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAECig8mZk02IcXRRoqkarhvI1YzRX9pLpbK3VHlhd73rYcxa7Dtye6E92Xt4vPdVrhw==", null, false, "ff61c724-f055-4dab-8a9a-593f46f92198", false, "doctor" },
                    { "b08a40cd-bc9a-44a4-9f4d-712e5b1ada9c", 0, "db596bb8-9282-4d3e-be67-fe1fed34be17", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAENm777T1hXa5r6A52MK5LnpsLhqvdk7wF+LObA+WN8nNOTlwSqFLc+8PEmpUENfbZA==", null, false, "35b39630-28b4-4c5e-bdcb-c26c974305dd", false, "technician" },
                    { "b73e82e5-542f-4199-ad69-78c7594a5298", 0, "a41e1477-c358-4b36-a8e7-4bed9b03a171", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEEMoO7WEyLq6RdV9USCnbOxmw4wE0ALCO5iijgVtqs7Dj5Kd6dHTK5XD6S3mbDSyPg==", null, false, "8e82618a-6e5e-4b96-b29c-7f84877d82e5", false, "nurse" },
                    { "beab97e8-5278-47e9-b81c-0dea8ad7ae6b", 0, "6e37c64e-bfbb-4ccf-882a-a474cd5dc9be", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEL8emMyj+2j25QnbIgEeon4AtKtMBVnl3iyBgUE4vNGH+VSzwW8EhN9HJ1BKvk3vcg==", null, false, "66837456-2dad-41b2-8d5d-23bf9ad74ad5", false, "patient" },
                    { "cd650ec9-74a0-4c08-b1d2-9735dfa710a1", 0, "528db32f-e0fc-4fd9-a02c-cad1c00a4b0c", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEEQ6MXkn1ZS8nFsEu5pt7uS7O+Q0tNZTeHSJLAWfKdnRuNGLuVxbo4vDDWiii200ig==", null, false, "9cb5bf7e-3f30-43d3-9e15-7e745cdc1ca3", false, "admin" }
>>>>>>>> fe98147 (Commit para cleaning):Migrations/20241023145657_InitialIdentity.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241023224044_InitialIdentity.cs
                    { "fd494d48-a044-4191-9131-d14b4f25ff42", "633173eb-d633-4456-a98b-8f0dbbc95606" },
                    { "b12fd2fb-878c-4f65-8d1e-ee4a04c6cea2", "70b77d4c-86a7-4f46-b678-bde105ecb63d" },
                    { "6bc4e754-5d0e-49f1-a1ae-4b8917be8461", "78b8acfd-8c8f-41b5-9d30-3cb3b936268c" },
                    { "43aaea9f-bf31-4c1c-82e0-30ab390acd4f", "93bd1d2d-aa93-4def-8ad5-774a92c58ec1" },
                    { "b04d8fd4-a52c-4f6d-a14d-401af7342c94", "93f9b31e-0f8c-49a6-9832-2b931dec97bc" }
========
                    { "ca6eb8e2-4d51-4824-8fac-05b98adc067b", "340e4ab6-9d69-41d2-826a-c8f3f2eabf2c" },
                    { "b9fc8d2c-f4ca-46bc-9803-46a891ec31ac", "b08a40cd-bc9a-44a4-9f4d-712e5b1ada9c" },
                    { "99be996b-5516-48ef-a70a-e3f49c0304c9", "b73e82e5-542f-4199-ad69-78c7594a5298" },
                    { "2ec62ad5-96a8-4929-9514-58ff2ecd9cba", "beab97e8-5278-47e9-b81c-0dea8ad7ae6b" },
                    { "531e44b0-e9a0-42de-9546-19864bee5624", "cd650ec9-74a0-4c08-b1d2-9735dfa710a1" }
>>>>>>>> fe98147 (Commit para cleaning):Migrations/20241023145657_InitialIdentity.cs
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
