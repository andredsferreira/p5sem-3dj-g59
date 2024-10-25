using System;
using DDDSample1.Domain.OperationTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.OperationTypes;
internal class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType> {
    public void Configure(EntityTypeBuilder<OperationType> builder) {
        builder.ToTable("OperationType", SchemaNames.DDDSample1);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.AsGuid(), x => new OperationTypeId(x));

        builder.Property(x => x.name)
            .HasConversion(x => x.name, x => new OperationName(x));

        builder.HasIndex(x => x.name)
            .IsUnique();

        builder.Property(x => x.anaesthesiaTime)
            .HasConversion(x => x.duration, x => new AnaesthesiaTime(x));

        builder.Property(x => x.surgeryTime)
            .HasConversion(x => x.duration, x => new SurgeryTime(x));

        builder.Property(x => x.cleaningTime)
            .HasConversion(x => x.duration, x => new CleaningTime(x));

        builder.Property(x=>x.Status)
            .HasConversion(x => x.ToString(), x => (Status)Enum.Parse(typeof(Status),x));

    }
}