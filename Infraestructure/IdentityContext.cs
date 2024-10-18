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

    // protected override void OnModelCreating(ModelBuilder builder) {
    //     base.OnModelCreating(builder);
    //     var userA = new IdentityUser();
    //     var passwordHasher = new PasswordHasher<IdentityUser>();
    //     userA.UserName = "andre";
    //     userA.Email = "andre@gmail.com";
    //     userA.PasswordHash = passwordHasher.HashPassword(userA, "password");
    //     builder.Entity<IdentityUser>().HasData(userA);
    // }

}