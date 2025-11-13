using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Available Glucose Measurements Endpoints")]
public class GlucoseMeasurementsController : ControllerBase
{
    private readonly IGlucoseMeasurementCommandService _commandService;
    private readonly IGlucoseMeasurementQueryService _queryService;

    public GlucoseMeasurementsController(
        IGlucoseMeasurementCommandService commandService,
        IGlucoseMeasurementQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all glucose measurements", OperationId = "GetAllGlucoseMeasurements")]
    [SwaggerResponse(200, "List of glucose measurements", typeof(IEnumerable<GlucoseMeasurement>))]
    public async Task<ActionResult<IEnumerable<GlucoseMeasurement>>> GetAll()
    {
        var measurements = await _queryService.Handle();
        return Ok(measurements);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get glucose measurement by id", OperationId = "GetGlucoseMeasurementById")]
    [SwaggerResponse(200, "Glucose measurement returned", typeof(GlucoseMeasurement))]
    [SwaggerResponse(404, "Glucose measurement not found")]
    public async Task<ActionResult<GlucoseMeasurement>> GetById(int id)
    {
        var measurement = await _queryService.Handle(id);
        if (measurement == null) return NotFound();
        return Ok(measurement);
    }

    [HttpGet("latest")]
    [SwaggerOperation(Summary = "Get latest glucose measurement", OperationId = "GetLatestGlucoseMeasurement")]
    [SwaggerResponse(200, "Latest glucose measurement returned", typeof(GlucoseMeasurement))]
    [SwaggerResponse(404, "No measurements found")]
    public async Task<ActionResult<GlucoseMeasurement>> GetLatest()
    {
        var measurement = await _queryService.GetLatestAsync();
        if (measurement == null) return NotFound();
        return Ok(measurement);
    }

    [HttpGet("recent")]
    [SwaggerOperation(Summary = "Get recent glucose measurements", OperationId = "GetRecentGlucoseMeasurements")]
    [SwaggerResponse(200, "List of recent glucose measurements", typeof(IEnumerable<GlucoseMeasurement>))]
    public async Task<ActionResult<IEnumerable<GlucoseMeasurement>>> GetRecent([FromQuery] int count = 7)
    {
        var measurements = await _queryService.GetRecentAsync(count);
        return Ok(measurements);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create glucose measurement", OperationId = "CreateGlucoseMeasurement")]
    [SwaggerResponse(201, "Glucose measurement created", typeof(GlucoseMeasurement))]
    public async Task<ActionResult<GlucoseMeasurement>> Create([FromBody] CreateGlucoseMeasurementCommand command)
    {
        var measurement = await _commandService.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id = measurement.Id }, measurement);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update glucose measurement", OperationId = "UpdateGlucoseMeasurement")]
    [SwaggerResponse(200, "Glucose measurement updated", typeof(GlucoseMeasurement))]
    [SwaggerResponse(404, "Glucose measurement not found")]
    public async Task<ActionResult<GlucoseMeasurement>> Update(int id, [FromBody] UpdateGlucoseMeasurementCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        try
        {
            var measurement = await _commandService.Handle(command);
            return Ok(measurement);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete glucose measurement", OperationId = "DeleteGlucoseMeasurement")]
    [SwaggerResponse(204, "Glucose measurement deleted")]
    [SwaggerResponse(404, "Glucose measurement not found")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _commandService.Handle(id);
        if (!result) return NotFound();
        return NoContent();
    }
}