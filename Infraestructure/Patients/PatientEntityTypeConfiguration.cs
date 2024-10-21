using System;
using System.Collections.Generic;
using System.Linq;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDSample1.Infrastructure.Patients;

internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient> {

    public void Configure(EntityTypeBuilder<Patient> builder) {
        builder.ToTable("Patient", SchemaNames.DDDSample1);

        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new MedicalRecordNumber(value));

        builder.Property(p => p.DateOfBirth);

        builder.Property(p => p.Email).HasMaxLength(255);

        builder.Property(p => p.FullName)
            .HasConversion(fullname => fullname.Full, full => new FullName(full));

        builder.Property(p => p.PhoneNumber).HasMaxLength(15);

        builder.HasIndex(p => p.Email).IsUnique();

        builder.HasIndex(p => p.PhoneNumber).IsUnique();


        var stringListValueConverter = new ValueConverter<List<string>, string>(
            list => string.Join(", ", list),
            str => string.IsNullOrWhiteSpace(str)
                ? new List<string>()
                : str.Split(new[] { ',' }, StringSplitOptions.None)
                    .Select(s => s.Trim())
                    .ToList());

        builder.Property(p => p.Allergies).HasConversion(stringListValueConverter);
    }
}
