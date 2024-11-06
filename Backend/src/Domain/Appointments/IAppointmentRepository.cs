using Backend.Domain.Shared;

namespace Backend.Domain.Appointments {

    public interface IAppointmentRepository : IRepository<Appointment, AppointmentId> {

    }

}