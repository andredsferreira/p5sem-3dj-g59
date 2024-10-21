using DDDSample1.Domain.OperationTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.OperationTypes {
    internal class OperationTypeEntityTypeConfiguration : IEntityTypeConfiguration<OperationType> {
        public void Configure(EntityTypeBuilder<OperationType> builder) {
            builder.ToTable("OperationType", SchemaNames.DDDSample1);

            // Primary Key
            builder.HasKey(x => x.Id);

            // Operation name
            builder.Property(x => x.name)
                .HasConversion(x => x.name, x => new OperationName(x));

            // Anasthesia time
            builder.Property(x => x.anaesthesiaTime)
                .HasConversion(x => x.duration, x => new AnaesthesiaTime(x));

            // Surgery time
            builder.Property(x => x.surgeryTime)
                            .HasConversion(x => x.duration, x => new SurgeryTime(x));

            // Clean time
            builder.Property(x => x.cleaningTime)
                            .HasConversion(x => x.duration, x => new CleaningTime(x));

        }
    }
}
