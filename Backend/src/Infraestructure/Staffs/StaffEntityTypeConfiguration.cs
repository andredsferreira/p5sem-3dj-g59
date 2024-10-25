using DDDSample1.Domain.Staffs;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Staffs;

internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff> {

    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Staff> builder) {
        builder.ToTable("Staff", SchemaNames.DDDSample1);

        builder.HasKey(staff => staff.Id);
        builder.Property(staff => staff.Id)
            .HasConversion(id => id.AsGuid(), value => new StaffId(value));

        builder.Property(staff => staff.identityUsername)
            .IsRequired();

    }
}