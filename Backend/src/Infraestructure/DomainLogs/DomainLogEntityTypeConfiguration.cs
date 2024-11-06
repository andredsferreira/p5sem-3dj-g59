using System;
using Backend.Domain.DomainLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.DomainLogs;

internal class DomainLogEntityTypeConfiguration : IEntityTypeConfiguration<DomainLog> {

    public void Configure(EntityTypeBuilder<DomainLog> builder) {
        builder.ToTable("DomainLog", SchemaNames.DDDSample1);

        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.AsGuid(),
            value => new DomainLogId(value));

        builder.Property(p => p.ObjectType)
            .HasConversion(objectType => objectType.ToString(), objectType => (LogObjectType)Enum.Parse(typeof(LogObjectType), objectType));

        builder.Property(p => p.ActionType)
            .HasConversion(actionType => actionType.ToString(), actionType => (LogActionType)Enum.Parse(typeof(LogActionType), actionType));

        builder.Property(p => p.Message)
            .HasMaxLength(255);

    }
}
