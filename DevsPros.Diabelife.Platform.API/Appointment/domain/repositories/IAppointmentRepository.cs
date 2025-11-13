using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Appointment.Domain.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<AppointmentEntity>> GetAllAsync();
    Task<AppointmentEntity?> GetByIdAsync(int id);
    Task<AppointmentEntity> AddAsync(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAsync(AppointmentEntity appointment);
    Task DeleteAsync(int id);
    Task<IEnumerable<AppointmentEntity>> GetByPatientAsync(string patient);
    Task<IEnumerable<AppointmentEntity>> GetByDoctorAsync(string doctor);
    Task<IEnumerable<AppointmentEntity>> GetByDateAsync(DateTime date);
    Task<IEnumerable<AppointmentEntity>> GetByStatusAsync(string status);
    Task<IEnumerable<AppointmentEntity>> GetUpcomingAppointmentsAsync();
}