﻿// <auto-generated />
using System;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDDNetCore.Migrations
{
    [DbContext(typeof(IdentityContext))]
    [Migration("20241026104753_InitialIdentity")]
    partial class InitialIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "e2ef60cb-131d-4d15-a440-6148f588b2b2",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "1f39f2a4-1774-47f3-933e-fe19bc6e6541",
                            Name = "Doctor",
                            NormalizedName = "DOCTOR"
                        },
                        new
                        {
                            Id = "08abeac0-05ab-415a-98c6-626353503923",
                            Name = "Nurse",
                            NormalizedName = "NURSE"
                        },
                        new
                        {
                            Id = "00e92ff8-d873-4ede-8003-14c8def64d07",
                            Name = "Technician",
                            NormalizedName = "TECHNICIAN"
                        },
                        new
                        {
                            Id = "f594f5e7-51cf-4007-b548-d40dd5039120",
                            Name = "Patient",
                            NormalizedName = "PATIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "14bb7afe-9642-40dd-9991-c770e242d332",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "dbea5210-e45e-4a1a-9a37-6aa79854ae62",
                            Email = "admin@hospital.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@HOSPITAL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEGb3dDHtyE+yBoiEh1O5859a/5WiGtfuG4r7yvN39xo0qMABLakVS8Rg20HReAjkwA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9d1a42f2-7e14-452d-87c7-5b82d8ac8ddd",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "f3225d16-5e31-4f71-84ff-99e4f08fe878",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8cff5b95-f0d7-4548-abaa-405574fb5b17",
                            Email = "doctor@hospital.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "DOCTOR@HOSPITAL.COM",
                            NormalizedUserName = "DOCTOR",
                            PasswordHash = "AQAAAAIAAYagAAAAEKLanoUrU8c3/UI1BHoknbny9p7uiWg1bT1lUBZmKnypy3Rh+1rFAn4c8YzbByIjJg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4d9dd1ca-7b80-4266-8e52-3aa43b5429cd",
                            TwoFactorEnabled = false,
                            UserName = "doctor"
                        },
                        new
                        {
                            Id = "92bb0c32-f292-49df-88cc-947725e4e237",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e06cbf90-83df-4f32-af96-51292a36f3ec",
                            Email = "nurse@hospital.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "NURSE@HOSPITAL.COM",
                            NormalizedUserName = "NURSE",
                            PasswordHash = "AQAAAAIAAYagAAAAEOejRUcddzQc/mrQvncExrCZHf+Zino8hkQym7JHdHzAbkI7Vg0RZw41OZAsD9zyMg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bd73772e-ced4-47b6-8dc9-ecaef9b73f48",
                            TwoFactorEnabled = false,
                            UserName = "nurse"
                        },
                        new
                        {
                            Id = "0fb02a04-fd41-41e4-bc30-161cac93c319",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "77fc7db3-907b-412e-8d7f-7f1a2c84e34d",
                            Email = "technician@hospital.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "TECHNICIAN@HOSPITAL.COM",
                            NormalizedUserName = "TECHNICIAN",
                            PasswordHash = "AQAAAAIAAYagAAAAELcmonJUhhrl1Lr3wihNNbcUSnIsfgeXe183ifepmmDjukje6QmmAVMiR1QjdNIRSw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e8f0e183-8237-4053-8f67-00fff44b7a16",
                            TwoFactorEnabled = false,
                            UserName = "technician"
                        },
                        new
                        {
                            Id = "01f2bf65-0ab5-4e05-a4e2-82b53bfe555d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "cff4b0fd-6b5e-4917-b77a-939a6cf047f4",
                            Email = "patient@hospital.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "PATIENT@HOSPITAL.COM",
                            NormalizedUserName = "PATIENT",
                            PasswordHash = "AQAAAAIAAYagAAAAEOq+swF3I/9XKjHo79qPsu2UyLF0Sq63w6Sq30lDIO6cTKllpPAJB43mWgmGwXNPMw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5f2cb6b2-bcd1-4bc7-b526-04d606bca623",
                            TwoFactorEnabled = false,
                            UserName = "patient"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "14bb7afe-9642-40dd-9991-c770e242d332",
                            RoleId = "e2ef60cb-131d-4d15-a440-6148f588b2b2"
                        },
                        new
                        {
                            UserId = "f3225d16-5e31-4f71-84ff-99e4f08fe878",
                            RoleId = "1f39f2a4-1774-47f3-933e-fe19bc6e6541"
                        },
                        new
                        {
                            UserId = "92bb0c32-f292-49df-88cc-947725e4e237",
                            RoleId = "08abeac0-05ab-415a-98c6-626353503923"
                        },
                        new
                        {
                            UserId = "0fb02a04-fd41-41e4-bc30-161cac93c319",
                            RoleId = "00e92ff8-d873-4ede-8003-14c8def64d07"
                        },
                        new
                        {
                            UserId = "01f2bf65-0ab5-4e05-a4e2-82b53bfe555d",
                            RoleId = "f594f5e7-51cf-4007-b548-d40dd5039120"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}