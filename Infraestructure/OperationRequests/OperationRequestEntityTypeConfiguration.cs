using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.OperationRequests;

namespace DDDSample1.Infrastructure.OperationRequests {

    internal class OperationRequestEntityTypeConfiguration : IEntityTypeConfiguration<OperationRequest> {

        public void Configure(EntityTypeBuilder<OperationRequest> builder) {
            builder.ToTable("OperationType", SchemaNames.DDDSample1);

            builder.HasKey(x => x.Id);

        }

    }

}