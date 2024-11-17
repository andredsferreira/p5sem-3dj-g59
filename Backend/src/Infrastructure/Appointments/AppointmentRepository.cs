using Backend.Domain.Appointments;
using Backend.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Appointments;

public class AppointmentRepository : BaseRepository<Appointment, AppointmentId>, IAppointmentRepository {

    public AppointmentRepository(AppDbContext context) : base(context.Appointments) {
        
    }

    public AppointmentRepository(DbSet<Appointment> objs) : base(objs) {
        
    }
}

