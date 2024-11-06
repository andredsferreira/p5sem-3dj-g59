using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Domain.OperationRequests;
using Backend.Domain.Patients;
using Backend.Domain.Staffs;
using Backend.Domain.OperationTypes;
using System;

namespace Backend.Infrastructure.OperationRequests;

internal class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest> {

    public void Configure(EntityTypeBuilder<OperationRequest> builder) {
        builder.ToTable("OperationRequest", SchemaNames.DDDSample1);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.AsGuid(), x => new OperationRequestId(x));

        builder.HasOne<Patient>(x => x.patient)
             .WithMany(s => s.OperationRequests)
             .HasForeignKey(s => s.patientId)
             .IsRequired();

        builder.HasOne<Staff>(x => x.staff)
            .WithMany(s => s.OperationRequests)
            .HasForeignKey(s => s.staffId)
            .IsRequired();

        builder.HasOne<OperationType>(x => x.operationType)
            .WithMany(s => s.OperationRequests)
            .HasForeignKey(s => s.operationTypeId)
            .IsRequired();

        builder.Property(x => x.priority)
            .HasConversion(
                v => v.ToString(),
                v => (OperationRequestPriority)Enum
                    .Parse(typeof(OperationRequestPriority), v)
            );

        builder.Property(x => x.requestStatus)
            .HasConversion(
                v => v.ToString(),
                v => (OperationRequestStatus)Enum
                    .Parse(typeof(OperationRequestStatus), v)
            );

    }
}
