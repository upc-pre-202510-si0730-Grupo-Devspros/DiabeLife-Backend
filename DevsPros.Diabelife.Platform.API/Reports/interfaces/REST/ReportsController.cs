using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
[SwaggerTag("Reports Management")]
public class ReportsController : ControllerBase
{
    private readonly IReportCommandService _reportCommandService;
    private readonly IReportQueryService _reportQueryService;

    public ReportsController(IReportCommandService reportCommandService, IReportQueryService reportQueryService)
    {
        _reportCommandService = reportCommandService;
        _reportQueryService = reportQueryService;
    }

    private int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException("Invalid user token");
        }
        return userId;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all reports for current user",
        Description = "Returns all reports belonging to the authenticated user")]
    [SwaggerResponse(200, "Reports retrieved successfully", typeof(IEnumerable<ReportResponseDto>))]
    [SwaggerResponse(401, "Unauthorized")]
    public async Task<IActionResult> GetReports()
    {
        try
        {
            var userId = GetCurrentUserId();
            var reports = await _reportQueryService.GetReportsByUserIdAsync(userId);
            
            var response = reports.Select(r => new ReportResponseDto
            {
                Id = r.Id,
                Name = r.Name,
                Date = r.Date,
                Type = r.Type,
                Data = r.Data,
                Selected = r.Selected,
                Shared = r.Shared,
                UserId = r.UserId,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            });

            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new report",
        Description = "Creates a new health report for the authenticated user")]
    [SwaggerResponse(201, "Report created successfully")]
    [SwaggerResponse(400, "Invalid request data")]
    [SwaggerResponse(401, "Unauthorized")]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportDto createDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            await _reportCommandService.CreateReportAsync(createDto, userId);
            return CreatedAtAction(nameof(CreateReport), new { message = "Report created successfully" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get report by ID",
        Description = "Returns a specific report belonging to the authenticated user")]
    [SwaggerResponse(200, "Report retrieved successfully", typeof(ReportResponseDto))]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Report not found")]
    public async Task<IActionResult> GetReportById(int id)
    {
        try
        {
            var userId = GetCurrentUserId();
            var report = await _reportQueryService.GetReportByIdAsync(id, userId);

            if (report == null)
            {
                return NotFound(new { message = "Report not found or access denied" });
            }

            var response = new ReportResponseDto
            {
                Id = report.Id,
                Name = report.Name,
                Date = report.Date,
                Type = report.Type,
                Data = report.Data,
                Selected = report.Selected,
                Shared = report.Shared,
                UserId = report.UserId,
                CreatedAt = report.CreatedAt,
                UpdatedAt = report.UpdatedAt
            };

            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update report",
        Description = "Updates an existing report belonging to the authenticated user")]
    [SwaggerResponse(200, "Report updated successfully")]
    [SwaggerResponse(400, "Invalid request data")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Report not found")]
    public async Task<IActionResult> UpdateReport(int id, [FromBody] UpdateReportDto updateDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            await _reportCommandService.UpdateReportAsync(id, updateDto, userId);
            return Ok(new { message = "Report updated successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete report",
        Description = "Deletes an existing report belonging to the authenticated user")]
    [SwaggerResponse(200, "Report deleted successfully")]
    [SwaggerResponse(401, "Unauthorized")]
    [SwaggerResponse(404, "Report not found")]
    public async Task<IActionResult> DeleteReport(int id)
    {
        try
        {
            var userId = GetCurrentUserId();
            await _reportCommandService.DeleteReportAsync(id, userId);
            return Ok(new { message = "Report deleted successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("shared")]
    [SwaggerOperation(
        Summary = "Share/unshare reports",
        Description = "Updates sharing status for multiple reports belonging to the authenticated user")]
    [SwaggerResponse(200, "Reports sharing status updated successfully")]
    [SwaggerResponse(400, "Invalid request data")]
    [SwaggerResponse(401, "Unauthorized")]
    public async Task<IActionResult> ShareReports([FromBody] ShareReportsDto shareDto)
    {
        try
        {
            var userId = GetCurrentUserId();
            await _reportCommandService.ShareReportsAsync(shareDto, userId);
            return Ok(new { message = "Reports sharing status updated successfully" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}