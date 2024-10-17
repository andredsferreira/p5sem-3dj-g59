using DDDSample1.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Patients {

    internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient> {

        public void Configure(EntityTypeBuilder<Patient> builder) {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            builder.ToTable("Patient", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
            builder.OwnsOne(p => p.FullName, fn =>
            {
                fn.Property(f => f.FirstName).HasColumnName("FirstName");
                fn.Property(f => f.LastName).HasColumnName("LastName");
                fn.Property(f => f.MiddleNames).HasColumnName("MiddleNames"); // Map as required
            });
            builder.Property<bool>("_active").HasColumnName("Active");
        }
    }

}