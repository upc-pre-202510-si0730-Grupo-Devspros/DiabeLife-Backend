using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;

public interface IAppointmentQueryService
{
    Task<IEnumerable<AppointmentEntity>> Handle();
    Task<AppointmentEntity?> Handle(int id);
    Task<IEnumerable<AppointmentEntity>> GetByPatient(string patient);
    Task<IEnumerable<AppointmentEntity>> GetByDoctor(string doctor);
    Task<IEnumerable<AppointmentEntity>> GetByDate(DateTime date);
    Task<IEnumerable<AppointmentEntity>> GetByStatus(string status);
    Task<IEnumerable<AppointmentEntity>> GetUpcomingAppointments();
}