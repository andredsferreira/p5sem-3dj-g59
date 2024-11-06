using System;
using System.Net.Mail;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.Patients;

internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient> {

    public void Configure(EntityTypeBuilder<Patient> builder) {
        builder.ToTable("Patient", SchemaNames.DDDSample1);

        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new PatientId(value));

        builder.Property(b => b.MedicalRecordNumber)
            .HasConversion(id => id.Record, value => new MedicalRecordNumber(value)).IsRequired();

        builder.Property(p => p.DateOfBirth)
            .HasColumnType("date").IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .HasConversion(p => p.ToString(), p => new MailAddress(p)).IsRequired();

        builder.Property(p => p.FullName)
            .HasConversion(fullname => fullname.Full, full => new FullName(full)).IsRequired();

        builder.Property(p => p.Gender)
            .HasConversion(gender => gender.ToString(), genero => (Gender)Enum.Parse(typeof(Gender), genero)).IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(15)
            .HasConversion(p => p.value, p => new PhoneNumber(p)).IsRequired();

        builder.Property(p => p.UserEmail)
            .HasMaxLength(255)
            .HasConversion(p => p.ToString(), p => new MailAddress(p));    
        
        builder.HasIndex(p => p.MedicalRecordNumber).IsUnique();

        builder.HasIndex(p => p.Email).IsUnique();

        builder.HasIndex(p => p.UserEmail).IsUnique();

        builder.HasIndex(p => p.PhoneNumber).IsUnique();

        builder.HasMany(p => p.Allergies)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

    }
}
