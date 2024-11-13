using System;
using System.Net.Mail;
using Backend.Domain.Patients;
using Backend.Domain.Shared;
using Backend.Domain.SurgeryRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.SurgeryRooms;

internal class SurgeryRoomEntityTypeConfiguration : IEntityTypeConfiguration<SurgeryRoom> {

    public void Configure(EntityTypeBuilder<SurgeryRoom> builder) {
        builder.ToTable("SurgeryRoom", SchemaNames.DDDSample1);

        builder.HasKey(b => b.Id);
        builder.Property(p => p.Id).HasConversion(
            id => id.Value,
            value => new SurgeryRoomId(value));

        builder.Property(b => b.Number)
            .HasConversion(id => id.Number, value => new RoomNumber(value)).IsRequired();

        builder.Property(p => p.RoomType)
            .HasConversion(roomType => roomType.ToString(), type => (RoomType)Enum.Parse(typeof(RoomType), type)).IsRequired();

        builder.Property(p => p.RoomStatus)
            .HasConversion(roomStatus => roomStatus.ToString(), status => (RoomStatus)Enum.Parse(typeof(RoomStatus), status)).IsRequired();
        
        builder.HasIndex(p => p.Number).IsUnique();

        //builder.HasMany(p => p.Allergies)
        //    .WithOne()
        //    .OnDelete(DeleteBehavior.Cascade);

    }
}
