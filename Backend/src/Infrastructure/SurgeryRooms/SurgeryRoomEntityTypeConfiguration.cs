using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.Json;
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

        builder.Property(p => p.Capacity).IsRequired();

        builder.Property(p => p.AssignedEquipment)
            .HasConversion(
                equipment => JsonSerializer.Serialize(equipment, (JsonSerializerOptions)null),
                equipment => JsonSerializer.Deserialize<List<string>>(equipment, (JsonSerializerOptions)null))
            .HasColumnType("JSON")
            .IsRequired();

        /*builder.OwnsMany(
            p => p.MaintenanceSlots,
            a =>
            {
                a.WithOwner().HasForeignKey("SurgeryRoomId");
                a.Property(s => s.day).HasColumnName("MaintenanceDay").IsRequired();
                a.Property(s => s.begin).HasColumnName("MaintenanceStart").IsRequired();
                a.Property(s => s.end).HasColumnName("MaintenanceEnd").IsRequired();

                a.ToTable("SurgeryRoomMaintenanceSlots", SchemaNames.DDDSample1);
            });*/
        
        builder.HasIndex(p => p.Number).IsUnique();

        builder.Property(p => p.MaintenanceSlots)
            .HasConversion(
                slots => JsonSerializer.Serialize(slots, (JsonSerializerOptions)null),
                slots => JsonSerializer.Deserialize<List<string>>(slots, (JsonSerializerOptions)null))
            .HasColumnType("JSON")
            .IsRequired();
    }
}
