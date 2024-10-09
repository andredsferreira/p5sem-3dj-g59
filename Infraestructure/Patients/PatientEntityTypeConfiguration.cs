using DDDSample1.Domain.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDSample1.Infrastructure.Patients {

    internal class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient> {

        public void Configure(EntityTypeBuilder<Patient> builder) {
            throw new System.NotImplementedException();
        }
    }

}