using DDDSample1.Domain.Staffs;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure.Staffs {

    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff> {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Staff> builder) {
            throw new System.NotImplementedException();
        }
    }

}