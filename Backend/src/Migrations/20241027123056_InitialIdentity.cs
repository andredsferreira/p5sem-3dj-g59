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
<<<<<<<< HEAD:Backend/src/Migrations/20241027124120_InitialIdentity.cs
                    { "11354b95-f5da-4b49-8aa6-01298a36eeda", null, "Patient", "PATIENT" },
                    { "1b7d12d1-fc51-480b-b967-0ea735c59b0d", null, "Doctor", "DOCTOR" },
                    { "3995d494-55d6-4f4c-ba19-7ae318fd2a71", null, "Nurse", "NURSE" },
                    { "720e7751-1b25-4373-af14-e1a3840c4602", null, "Technician", "TECHNICIAN" },
                    { "b403fc49-6dd0-4b8e-8581-34de776cf50f", null, "Admin", "ADMIN" }
========
                    { "296eff06-0d15-45e3-8393-353cf539fc9b", null, "Patient", "PATIENT" },
                    { "41383035-7525-4b7e-aad1-2587242df310", null, "Admin", "ADMIN" },
                    { "8041e28d-37b3-4bd4-be4b-d0cc68305c8e", null, "Doctor", "DOCTOR" },
                    { "ecd40bce-116d-48d0-9220-ed2a67d6469b", null, "Technician", "TECHNICIAN" },
                    { "ed981cbd-958e-4e13-809b-ccd18f325081", null, "Nurse", "NURSE" }
>>>>>>>> af32e17 (Save before):Backend/src/Migrations/20241027123056_InitialIdentity.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241027124120_InitialIdentity.cs
                    { "2edc30dd-2faf-44b7-9fba-2bc499b1caab", 0, "1e6efdfd-cf3b-4142-97bf-878a26345026", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEE2bU5EuaaJ0at4XSKn3ZiGGUguzpxcZnLn5YNtAwZtD/NDqTDiSs56xooIXjTYG1Q==", null, false, "c20e0f37-87b0-4ac6-befe-1dd8872a3d5a", false, "doctor" },
                    { "400a776e-a6ab-4f05-9f7b-747a48f53b13", 0, "e8bd675e-f461-46e6-8887-96a2e812fb4e", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAED1TF/X99gWFbOhso1SWjeWIg72Do6OBGButghHWS52N1TdodwWKBPJwPJf2HYEkVQ==", null, false, "0aea72b6-ca5f-42df-b6cb-85e19fb878e4", false, "nurse" },
                    { "81b7e5d1-3902-4024-974f-9e6c99f3c052", 0, "44e3d210-4d2b-4612-a694-f0ae234afce4", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAELqI2zz0EzCbR4TWpmWBbzBgzZxTBdSkeTZ1Tok7HGOUAdS6A+MvLUti4paGg5ZafQ==", null, false, "b1b90108-ae1d-414f-8005-0b573e789915", false, "technician" },
                    { "96c7ab22-cfbc-4a6f-a6aa-23254e9898c4", 0, "cc62a30f-df71-46be-ae62-98a43ab5cb4f", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEDizXXzlknuPK6Ex7jZ1A6nZEJNZrXNWFkPMoNP3sdQC2JMnDxyr93tpTj2X60YrDA==", null, false, "23cc3ce3-d9c9-43e8-8446-e1d536011369", false, "admin" },
                    { "9b7ecd5e-b88a-4131-8ed4-f8915b16b44b", 0, "d55c8675-d659-4e45-86ac-89a925da3567", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEB1dR9hO9NKaEovLz1vCTTaOYrS9aL/9CutVN70eAzistiAzqCUSac37sECWACKOjw==", null, false, "c7ce1fb5-7d80-4c55-92fc-93e174c0862a", false, "patient" }
========
                    { "058a1238-c071-41bf-9cc8-a8d28711752d", 0, "bccc5312-08fe-4086-8a9e-82e14fcd381b", "doctor@hospital.com", true, false, null, "DOCTOR@HOSPITAL.COM", "DOCTOR", "AQAAAAIAAYagAAAAEN0oWXYd1H7j0zlvI+XL5NW5nhk/8/kkSjmwzWxAWhVa2G8HloyoMmmT7BVWDnKMPw==", null, false, "0c0f112d-f9b0-4eac-8da6-50761f48cb9c", false, "doctor" },
                    { "12b413aa-cf9b-4d4d-b16b-1ee4bead2b91", 0, "ec77e5ff-d250-4b10-b996-0d8e94bd95b9", "patient@hospital.com", true, false, null, "PATIENT@HOSPITAL.COM", "PATIENT", "AQAAAAIAAYagAAAAEDcXWEYrjaaHlBL+2caSh4WIF+n4zMY6lBUrqjRWTsojhq4NBUvFBVaIYxxfGDgD0Q==", null, false, "00e1192f-928e-4fd0-8309-a3500ffa3079", false, "patient" },
                    { "3ee2d631-09f1-4b46-bc6b-87a79b6da5b4", 0, "ed589ad6-13b6-4138-bbba-4883e0f4d689", "admin@hospital.com", true, false, null, "ADMIN@HOSPITAL.COM", "ADMIN", "AQAAAAIAAYagAAAAEFERiNrgdzMLW7AwlfA6w7mTpXAXpZDg33SPKvf33WS3deiIFTjbpXz+daIkjGEB5g==", null, false, "f693aa6d-0426-4210-ae00-94241f49ee3a", false, "admin" },
                    { "4bf25d5d-41f4-4816-aef4-d8d23ca8415d", 0, "0cfbc430-c2ff-43ee-a404-6576759a9c68", "nurse@hospital.com", true, false, null, "NURSE@HOSPITAL.COM", "NURSE", "AQAAAAIAAYagAAAAEH4/EkEggIWkRXe+o5QUDSVhu9wy67Qvaha1UfI7HnkPEN5ooold3yDPIf1Ye6jA3w==", null, false, "ae92c968-4d1b-43d3-883a-dcc2f3ce3f20", false, "nurse" },
                    { "7bbd586e-2339-4eb6-a0c2-97aac9df9053", 0, "a2fc7210-bff4-4d64-8f41-2ba886afbde2", "technician@hospital.com", true, false, null, "TECHNICIAN@HOSPITAL.COM", "TECHNICIAN", "AQAAAAIAAYagAAAAEELXAypMgDdFBt296lXm8Iy3iw5Vl8CCfORWNlZfaw5soYDbMVK1Fr3x/vsv+Kgg7A==", null, false, "0d994cec-ba4c-42de-aa94-73873bf478e2", false, "technician" }
>>>>>>>> af32e17 (Save before):Backend/src/Migrations/20241027123056_InitialIdentity.cs
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
<<<<<<<< HEAD:Backend/src/Migrations/20241027124120_InitialIdentity.cs
                    { "1b7d12d1-fc51-480b-b967-0ea735c59b0d", "2edc30dd-2faf-44b7-9fba-2bc499b1caab" },
                    { "3995d494-55d6-4f4c-ba19-7ae318fd2a71", "400a776e-a6ab-4f05-9f7b-747a48f53b13" },
                    { "720e7751-1b25-4373-af14-e1a3840c4602", "81b7e5d1-3902-4024-974f-9e6c99f3c052" },
                    { "b403fc49-6dd0-4b8e-8581-34de776cf50f", "96c7ab22-cfbc-4a6f-a6aa-23254e9898c4" },
                    { "11354b95-f5da-4b49-8aa6-01298a36eeda", "9b7ecd5e-b88a-4131-8ed4-f8915b16b44b" }
========
                    { "8041e28d-37b3-4bd4-be4b-d0cc68305c8e", "058a1238-c071-41bf-9cc8-a8d28711752d" },
                    { "296eff06-0d15-45e3-8393-353cf539fc9b", "12b413aa-cf9b-4d4d-b16b-1ee4bead2b91" },
                    { "41383035-7525-4b7e-aad1-2587242df310", "3ee2d631-09f1-4b46-bc6b-87a79b6da5b4" },
                    { "ed981cbd-958e-4e13-809b-ccd18f325081", "4bf25d5d-41f4-4816-aef4-d8d23ca8415d" },
                    { "ecd40bce-116d-48d0-9220-ed2a67d6469b", "7bbd586e-2339-4eb6-a0c2-97aac9df9053" }
>>>>>>>> af32e17 (Save before):Backend/src/Migrations/20241027123056_InitialIdentity.cs
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
