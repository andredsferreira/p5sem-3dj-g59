using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DDDSample1.Domain.DomainLogs;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDDSample1.Infrastructure.DomainLogs;

internal class DomainLogEntityTypeConfiguration : IEntityTypeConfiguration<DomainLog> {

    public void Configure(EntityTypeBuilder<DomainLog> builder) {
        builder.ToTable("DomainLog", SchemaNames.DDDSample1);

        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new DomainLogId(value));

        builder.Property(p => p.ObjectType)
            .HasConversion(objectType => objectType.ToString(), objectType => (LogObjectType)Enum.Parse(typeof(LogObjectType), objectType));

        builder.Property(p => p.ActionType)
            .HasConversion(actionType => actionType.ToString(), actionType => (LogActionType)Enum.Parse(typeof(LogActionType), actionType));

        builder.Property(p => p.Message)
            .HasMaxLength(255);

    }
}
