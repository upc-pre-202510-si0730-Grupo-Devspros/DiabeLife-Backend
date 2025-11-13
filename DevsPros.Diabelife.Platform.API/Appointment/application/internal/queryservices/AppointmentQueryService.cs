using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.QueryServices;

internal class AppointmentQueryService : IAppointmentQueryService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentQueryService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<AppointmentEntity>> Handle()
    {
        return await _appointmentRepository.GetAllAsync();
    }

    public async Task<AppointmentEntity?> Handle(int id)
    {
        return await _appointmentRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByPatient(string patient)
    {
        return await _appointmentRepository.GetByPatientAsync(patient);
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByDoctor(string doctor)
    {
        return await _appointmentRepository.GetByDoctorAsync(doctor);
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByDate(DateTime date)
    {
        return await _appointmentRepository.GetByDateAsync(date);
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByStatus(string status)
    {
        return await _appointmentRepository.GetByStatusAsync(status);
    }

    public async Task<IEnumerable<AppointmentEntity>> GetUpcomingAppointments()
    {
        return await _appointmentRepository.GetUpcomingAppointmentsAsync();
    }
}