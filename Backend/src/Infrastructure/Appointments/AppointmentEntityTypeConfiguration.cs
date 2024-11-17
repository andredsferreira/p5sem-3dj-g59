using System;
using Backend.Domain.Appointments;
using Backend.Domain.OperationRequests;
using Backend.Domain.SurgeryRooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infrastructure.OperationTypes;
internal class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment> {
    public void Configure(EntityTypeBuilder<Appointment> builder) {
        builder.ToTable("Appointment", SchemaNames.DDDSample1);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.AsGuid(), x => new AppointmentId(x));

        builder.HasOne<SurgeryRoom>(x => x.SurgeryRoom)
             .WithMany(s => s.Appointments)
             .HasForeignKey(s => s.SurgeryRoomId)
             .IsRequired();

        builder.HasOne<OperationRequest>(x => x.OperationRequest)
            .WithMany(s => s.Appointments)
            .HasForeignKey(s => s.OperationRequestId)
            .IsRequired();

        builder.Property(x => x.AppointmentStatus)
            .HasConversion(x => x.ToString(), x => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), x)).IsRequired();

        builder.Property(p => p.DateTime)
            .HasConversion(p => p.ToString(), p => DateTime.Parse(p)).IsRequired();
    }
}