using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Domain.OperationRequests;
using Backend.Domain.Patients;
using Backend.Domain.Staffs;
using Backend.Domain.OperationTypes;

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

        // builder.Property(x => x.patientId)
        //     .HasConversion(x => x.AsGuid(), x => new PatientId(x));

        // builder.Property(x => x.staffId)
        //     .HasConversion(x => x.AsGuid(), x => new StaffId(x));

        // builder.Property(x => x.operationTypeId)
        //     .HasConversion(x => x.AsGuid(), x => new OperationTypeId(x));

    }
}