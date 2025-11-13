namespace DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.CommandServices;

public record CreateAppointmentCommand(DateTime AppointmentDate, string Doctor, string Patient, string AppointmentType, 
    string Location, int Duration, string Notes);

public record UpdateAppointmentCommand(int Id, DateTime AppointmentDate, string Doctor, string Patient, 
    string AppointmentType, string Location, int Duration, string Notes);

public record UpdateAppointmentStatusCommand(int Id, string Status);

public record AddAppointmentNotesCommand(int Id, string Notes);