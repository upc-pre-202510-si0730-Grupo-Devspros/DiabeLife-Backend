using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;

public interface IAppointmentCommandService
{
    Task<AppointmentEntity> Handle(CreateAppointmentCommand command);
    Task<AppointmentEntity> Handle(UpdateAppointmentCommand command);
    Task<AppointmentEntity> Handle(UpdateAppointmentStatusCommand command);
    Task<AppointmentEntity> Handle(AddAppointmentNotesCommand command);
    Task<bool> Handle(int id);
}