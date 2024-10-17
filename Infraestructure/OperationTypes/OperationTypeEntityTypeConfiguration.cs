using DDDSample1.Domain.OperationTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.OperationTypes{

    internal class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType> {

        public void Configure(EntityTypeBuilder<OperationType> builder) {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            builder.ToTable("OperationType", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
            builder.Property(b => b.name).HasColumnName("Name");
            builder.Property(b => b.estimatedDuration).HasColumnName("EstimatedDuration");
            
        }



    }

}