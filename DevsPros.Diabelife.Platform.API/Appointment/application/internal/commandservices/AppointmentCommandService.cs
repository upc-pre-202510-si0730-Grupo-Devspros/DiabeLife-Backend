using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.CommandServices;

internal class AppointmentCommandService : IAppointmentCommandService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentCommandService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<AppointmentEntity> Handle(CreateAppointmentCommand command)
    {
        var appointment = new AppointmentEntity(command.AppointmentDate, command.Doctor, command.Patient, 
            command.AppointmentType, command.Location, command.Duration, command.Notes);
        return await _appointmentRepository.AddAsync(appointment);
    }

    public async Task<AppointmentEntity> Handle(UpdateAppointmentCommand command)
    {
        var existingAppointment = await _appointmentRepository.GetByIdAsync(command.Id);
        if (existingAppointment == null)
            throw new Exception($"Appointment with id {command.Id} not found");

        existingAppointment.UpdateAppointment(command.AppointmentDate, command.Doctor, command.Patient,
            command.AppointmentType, command.Location, command.Duration, command.Notes);
        return await _appointmentRepository.UpdateAsync(existingAppointment);
    }

    public async Task<AppointmentEntity> Handle(UpdateAppointmentStatusCommand command)
    {
        var existingAppointment = await _appointmentRepository.GetByIdAsync(command.Id);
        if (existingAppointment == null)
            throw new Exception($"Appointment with id {command.Id} not found");

        existingAppointment.UpdateStatus(command.Status);
        return await _appointmentRepository.UpdateAsync(existingAppointment);
    }

    public async Task<AppointmentEntity> Handle(AddAppointmentNotesCommand command)
    {
        var existingAppointment = await _appointmentRepository.GetByIdAsync(command.Id);
        if (existingAppointment == null)
            throw new Exception($"Appointment with id {command.Id} not found");

        existingAppointment.AddNotes(command.Notes);
        return await _appointmentRepository.UpdateAsync(existingAppointment);
    }

    public async Task<bool> Handle(int id)
    {
        try
        {
            await _appointmentRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}