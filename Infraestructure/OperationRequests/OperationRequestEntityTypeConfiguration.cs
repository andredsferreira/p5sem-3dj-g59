using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.OperationRequests;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Staffs;
using DDDSample1.Domain.OperationTypes;

namespace DDDSample1.Infrastructure.OperationRequests {

    internal class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest> {

        public void Configure(EntityTypeBuilder<OperationRequest> builder) {
            builder.ToTable("OperationRequest", SchemaNames.DDDSample1);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.AsGuid(), x => new OperationRequestId(x));

           builder.HasOne<Patient>(x => x.patient)
                .WithMany(s => s.OperationRequests);

            builder.HasOne<Staff>(x => x.staff)
                .WithMany(s => s.OperationRequests);

            builder.HasOne<OperationType>(x => x.operationType)
                .WithMany(s => s.OperationRequests);

            // builder.Property(x => x.patientRecordNumber)
            //     .HasConversion(x => x.AsGuid(), x => new MedicalRecordNumber(x));

            // builder.Property(x => x.staffId)
            //     .HasConversion(x => x.AsGuid(), x => new StaffId(x));

            // builder.Property(x => x.operationTypeId)
            //     .HasConversion(x => x.AsGuid(), x => new OperationTypeId(x));

        }
    }

}