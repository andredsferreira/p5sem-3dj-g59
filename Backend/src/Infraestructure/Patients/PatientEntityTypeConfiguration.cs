using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDSample1.Infrastructure.Patients;

internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient> {

    public void Configure(EntityTypeBuilder<Patient> builder) {
        builder.ToTable("Patient", SchemaNames.DDDSample1);

        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new PatientId(value));

        builder.Property(b => b.MedicalRecordNumber)
            .HasConversion(id => id.Record, value => new MedicalRecordNumber(value));

        builder.Property(p => p.DateOfBirth)
            .HasColumnType("date");

        builder.Property(p => p.Email)
            .HasMaxLength(255);

        builder.Property(p => p.FullName)
            .HasConversion(fullname => fullname.Full, full => new FullName(full));

        builder.Property(p => p.Gender)
            .HasConversion(gender => gender.ToString(), genero => (Gender)Enum.Parse(typeof(Gender), genero));

        builder.Property(p => p.PhoneNumber).HasMaxLength(15);
        
        builder.HasIndex(p => p.MedicalRecordNumber).IsUnique();

        builder.HasIndex(p => p.Email).IsUnique();

        builder.HasIndex(p => p.PhoneNumber).IsUnique();

        builder.HasMany(p => p.Allergies)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

    }
}
