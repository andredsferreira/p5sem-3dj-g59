using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDDSample1.Domain.Auth;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DDDSample1.Infrastructure.Auth {

    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User> {

        public void Configure(EntityTypeBuilder<User> builder) {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx
            
            builder.ToTable("User", SchemaNames.DDDSample1);
            builder.HasKey(b => b.Id);
            
            builder.Property<bool>("_active").HasColumnName("Active");
        }

    }

}