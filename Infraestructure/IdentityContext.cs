using System;
using DDDSample1.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DDDSample1.Infrastructure;

public class IdentityContext : IdentityDbContext<IdentityUser> {

    private readonly IConfiguration configuration;

    public IdentityContext(IConfiguration configuration) {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        // Bootstrap das roles na base de dados
        string adminRoleId = Guid.NewGuid().ToString();
        string doctorRoleId = Guid.NewGuid().ToString();
        string nurseRoleId = Guid.NewGuid().ToString();
        string technicianRoleId = Guid.NewGuid().ToString();
        string patientRoleId = Guid.NewGuid().ToString();

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole {
                Id = adminRoleId,
                Name = HospitalRoles.Admin,
                NormalizedName = HospitalRoles.Admin.ToUpper()
            }, new IdentityRole {
                Id = doctorRoleId,
                Name = HospitalRoles.Doctor,
                NormalizedName = HospitalRoles.Doctor.ToUpper()
            }, new IdentityRole {
                Id = nurseRoleId,
                Name = HospitalRoles.Nurse,
                NormalizedName = HospitalRoles.Nurse.ToUpper()
            }, new IdentityRole {
                Id = technicianRoleId,
                Name = HospitalRoles.Technician,
                NormalizedName = HospitalRoles.Technician.ToUpper()
            }, new IdentityRole {
                Id = patientRoleId,
                Name = HospitalRoles.Patient,
                NormalizedName = HospitalRoles.Patient.ToUpper()
            }
        );

        // Bootsrap de utilizadores usando o método de auxílio SeedHospitalUser():
        SeedHospitalUser(builder, adminRoleId, "admin", "admin@hospital.com", "adminpassword");
        SeedHospitalUser(builder, doctorRoleId, "doctor", "doctor@hospital.com", "doctorpassword");
        SeedHospitalUser(builder, nurseRoleId, "nurse", "nurse@hospital.com", "nursepassword");
        SeedHospitalUser(builder, technicianRoleId, "technician", "technician@hospital.com", "technicianpassword");
        SeedHospitalUser(builder, patientRoleId, "patient", "patient@hospital.com", "patientpassword");

    }

    private void SeedHospitalUser(ModelBuilder builder, string roleId, string username, string email, string password) {
        string adminUserId = Guid.NewGuid().ToString();
        var adminUser = new IdentityUser {
            Id = adminUserId,
            UserName = username,
            NormalizedUserName = username.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var passwordHasher = new PasswordHasher<IdentityUser>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, password);

        builder.Entity<IdentityUser>().HasData(adminUser);

        // Atribuir admin role ao admin user
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> {
                RoleId = roleId,
                UserId = adminUserId
            }
        );
    }

}