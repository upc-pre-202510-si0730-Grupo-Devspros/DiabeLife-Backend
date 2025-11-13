using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Appointment.Infrastructure.Persistence.EFC.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentEntity>> GetAllAsync()
    {
        return await _context.Appointments.OrderByDescending(a => a.AppointmentDate).ToListAsync();
    }

    public async Task<AppointmentEntity?> GetByIdAsync(int id)
    {
        return await _context.Appointments.FindAsync(id);
    }

    public async Task<AppointmentEntity> AddAsync(AppointmentEntity appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<AppointmentEntity> UpdateAsync(AppointmentEntity appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByPatientAsync(string patient)
    {
        return await _context.Appointments
            .Where(a => a.Patient.Contains(patient))
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByDoctorAsync(string doctor)
    {
        return await _context.Appointments
            .Where(a => a.Doctor.Contains(doctor))
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByDateAsync(DateTime date)
    {
        return await _context.Appointments
            .Where(a => a.AppointmentDate.Date == date.Date)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppointmentEntity>> GetByStatusAsync(string status)
    {
        return await _context.Appointments
            .Where(a => a.Status == status)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppointmentEntity>> GetUpcomingAppointmentsAsync()
    {
        var now = DateTime.UtcNow;
        return await _context.Appointments
            .Where(a => a.AppointmentDate > now && a.Status == "Scheduled")
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();
    }
}