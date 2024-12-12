using System;
using Backend.Domain.Specializations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Specializations;

internal class SpecializationEntityTypeConfiguration : IEntityTypeConfiguration<Specialization> {
    public void Configure(EntityTypeBuilder<Specialization> builder) {

        builder.ToTable("Specialization", SchemaNames.DDDSample1);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.AsGuid(), x => new SpecializationID(x));

        builder.Property(x => x.codeSpec).IsRequired()
            .HasConversion(x => x.ToString(), x => new CodeSpec(x));

        builder.HasIndex(x => x.codeSpec)
            .IsUnique();       

        builder.Property(x => x.designation).IsRequired()
            .HasConversion(x => x.ToString(), x => new Designation(x));

        builder.HasIndex(x => x.designation)
            .IsUnique();
        
        builder.Property(x => x.description).IsRequired()
            .HasConversion(x => x.ToString(), x => new Description(x));
        
        
    }
}