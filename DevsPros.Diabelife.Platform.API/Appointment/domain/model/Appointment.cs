using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;

public class AppointmentEntity : BaseEntity
{
    public DateTime AppointmentDate { get; set; }
    public string Doctor { get; set; } = string.Empty;
    public string Patient { get; set; } = string.Empty;
    public string AppointmentType { get; set; } = string.Empty;
    public string Status { get; set; } = "Scheduled"; // Scheduled, Completed, Cancelled, Rescheduled
    public string Notes { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Duration { get; set; } // Duration in minutes

    public AppointmentEntity() { }

    public AppointmentEntity(DateTime appointmentDate, string doctor, string patient, string appointmentType, 
        string location, int duration, string notes = "")
    {
        AppointmentDate = appointmentDate;
        Doctor = doctor;
        Patient = patient;
        AppointmentType = appointmentType;
        Location = location;
        Duration = duration;
        Notes = notes;
        Status = "Scheduled";
    }

    public void UpdateAppointment(DateTime appointmentDate, string doctor, string patient, 
        string appointmentType, string location, int duration, string notes = "")
    {
        AppointmentDate = appointmentDate;
        Doctor = doctor;
        Patient = patient;
        AppointmentType = appointmentType;
        Location = location;
        Duration = duration;
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(string status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddNotes(string notes)
    {
        Notes = notes;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsScheduled() => Status == "Scheduled";
    public bool IsCompleted() => Status == "Completed";
    public bool IsCancelled() => Status == "Cancelled";
    public bool IsRescheduled() => Status == "Rescheduled";
}