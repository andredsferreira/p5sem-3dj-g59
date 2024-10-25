using System.Net.Mail;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Staffs;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Staffs;

internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff> {

    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Staff> builder) {
        builder.ToTable("Staff", SchemaNames.DDDSample1);

        builder.HasKey(staff => staff.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new StaffId(value));

        
        

        builder.Property(staff => staff.LicenseNumber)
            .HasConversion(id => id.Value, value => new LicenseNumber(value));

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .HasConversion(p => p.ToString(), p => new MailAddress(p));

        builder.Property(p => p.FullName)
            .HasConversion(fullname => fullname.Full, full => new FullName(full));

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(15)
            .HasConversion(p => p.value, p => new PhoneNumber(p));
            
        
        builder.HasIndex(p => p.LicenseNumber).IsUnique();

        builder.HasIndex(p => p.Email).IsUnique();

        builder.HasIndex(p => p.PhoneNumber).IsUnique();

        

        builder.Property(staff => staff.identityUsername)
            .IsRequired();

    }
}