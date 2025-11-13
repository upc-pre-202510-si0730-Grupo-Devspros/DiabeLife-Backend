using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.Appointment.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Available Appointment Endpoints")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentCommandService _appointmentCommandService;
    private readonly IAppointmentQueryService _appointmentQueryService;

    public AppointmentsController(
        IAppointmentCommandService appointmentCommandService,
        IAppointmentQueryService appointmentQueryService)
    {
        _appointmentCommandService = appointmentCommandService;
        _appointmentQueryService = appointmentQueryService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all appointments",
        Description = "Get all appointments from the database",
        OperationId = "GetAllAppointments")]
    [SwaggerResponse(200, "Appointments returned successfully", typeof(IEnumerable<AppointmentEntity>))]
    public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetAllAppointments()
    {
        var appointments = await _appointmentQueryService.Handle();
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get appointment by id",
        Description = "Get a specific appointment by its id",
        OperationId = "GetAppointmentById")]
    [SwaggerResponse(200, "Appointment returned successfully", typeof(AppointmentEntity))]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult<AppointmentEntity>> GetAppointmentById(int id)
    {
        var appointment = await _appointmentQueryService.Handle(id);
        if (appointment == null)
            return NotFound();
        return Ok(appointment);
    }

    [HttpGet("patient/{patient}")]
    [SwaggerOperation(
        Summary = "Get appointments by patient",
        Description = "Get all appointments for a specific patient",
        OperationId = "GetAppointmentsByPatient")]
    [SwaggerResponse(200, "Appointments returned successfully", typeof(IEnumerable<AppointmentEntity>))]
    public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetAppointmentsByPatient(string patient)
    {
        var appointments = await _appointmentQueryService.GetByPatient(patient);
        return Ok(appointments);
    }

    [HttpGet("doctor/{doctor}")]
    [SwaggerOperation(
        Summary = "Get appointments by doctor",
        Description = "Get all appointments for a specific doctor",
        OperationId = "GetAppointmentsByDoctor")]
    [SwaggerResponse(200, "Appointments returned successfully", typeof(IEnumerable<AppointmentEntity>))]
    public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetAppointmentsByDoctor(string doctor)
    {
        var appointments = await _appointmentQueryService.GetByDoctor(doctor);
        return Ok(appointments);
    }

    [HttpGet("date/{date}")]
    [SwaggerOperation(
        Summary = "Get appointments by date",
        Description = "Get all appointments for a specific date",
        OperationId = "GetAppointmentsByDate")]
    [SwaggerResponse(200, "Appointments returned successfully", typeof(IEnumerable<AppointmentEntity>))]
    public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetAppointmentsByDate(DateTime date)
    {
        var appointments = await _appointmentQueryService.GetByDate(date);
        return Ok(appointments);
    }

    [HttpGet("status/{status}")]
    [SwaggerOperation(
        Summary = "Get appointments by status",
        Description = "Get all appointments with a specific status",
        OperationId = "GetAppointmentsByStatus")]
    [SwaggerResponse(200, "Appointments returned successfully", typeof(IEnumerable<AppointmentEntity>))]
    public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetAppointmentsByStatus(string status)
    {
        var appointments = await _appointmentQueryService.GetByStatus(status);
        return Ok(appointments);
    }

    [HttpGet("upcoming")]
    [SwaggerOperation(
        Summary = "Get upcoming appointments",
        Description = "Get all scheduled appointments that are upcoming",
        OperationId = "GetUpcomingAppointments")]
    [SwaggerResponse(200, "Upcoming appointments returned successfully", typeof(IEnumerable<AppointmentEntity>))]
    public async Task<ActionResult<IEnumerable<AppointmentEntity>>> GetUpcomingAppointments()
    {
        var appointments = await _appointmentQueryService.GetUpcomingAppointments();
        return Ok(appointments);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create appointment",
        Description = "Create a new appointment",
        OperationId = "CreateAppointment")]
    [SwaggerResponse(201, "Appointment created successfully", typeof(AppointmentEntity))]
    [SwaggerResponse(400, "Invalid appointment data")]
    public async Task<ActionResult<AppointmentEntity>> CreateAppointment([FromBody] CreateAppointmentCommand command)
    {
        var appointment = await _appointmentCommandService.Handle(command);
        return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update appointment",
        Description = "Update an existing appointment",
        OperationId = "UpdateAppointment")]
    [SwaggerResponse(200, "Appointment updated successfully", typeof(AppointmentEntity))]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult<AppointmentEntity>> UpdateAppointment(int id, [FromBody] UpdateAppointmentCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        try
        {
            var appointment = await _appointmentCommandService.Handle(command);
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id}/status")]
    [SwaggerOperation(
        Summary = "Update appointment status",
        Description = "Update the status of an existing appointment",
        OperationId = "UpdateAppointmentStatus")]
    [SwaggerResponse(200, "Appointment status updated successfully", typeof(AppointmentEntity))]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult<AppointmentEntity>> UpdateAppointmentStatus(int id, [FromBody] UpdateAppointmentStatusCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        try
        {
            var appointment = await _appointmentCommandService.Handle(command);
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id}/notes")]
    [SwaggerOperation(
        Summary = "Add appointment notes",
        Description = "Add notes to an existing appointment",
        OperationId = "AddAppointmentNotes")]
    [SwaggerResponse(200, "Appointment notes added successfully", typeof(AppointmentEntity))]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult<AppointmentEntity>> AddAppointmentNotes(int id, [FromBody] AddAppointmentNotesCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id mismatch");

        try
        {
            var appointment = await _appointmentCommandService.Handle(command);
            return Ok(appointment);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete appointment",
        Description = "Delete an appointment",
        OperationId = "DeleteAppointment")]
    [SwaggerResponse(204, "Appointment deleted successfully")]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult> DeleteAppointment(int id)
    {
        var result = await _appointmentCommandService.Handle(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}