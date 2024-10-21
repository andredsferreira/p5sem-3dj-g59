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
        // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

        builder.ToTable("Patient", SchemaNames.DDDSample1);
        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new MedicalRecordNumber(value));

        builder.Property(p => p.DateOfBirth);
        builder.Property(p => p.Email).HasMaxLength(255);
        builder.Property(p => p.PhoneNumber).HasMaxLength(15);

        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasIndex(p => p.PhoneNumber).IsUnique();

        builder.Property(p => p.FullName).HasConversion(
            fullname => fullname.Full,
            full => new FullName(full));

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
