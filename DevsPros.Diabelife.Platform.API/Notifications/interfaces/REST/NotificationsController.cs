using DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace DevsPros.Diabelife.Platform.API.Notifications.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationCommandService _commandService;
    private readonly NotificationQueryService _queryService;

    public NotificationsController(NotificationCommandService commandService, NotificationQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetAllNotifications()
    {
        var notifications = await _queryService.Handle();
        return Ok(notifications);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> GetNotificationById(int id)
    {
        var notification = await _queryService.Handle(id);
        if (notification == null)
            return NotFound();
        return Ok(notification);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsByUserId(string userId)
    {
        var notifications = await _queryService.GetByUserIdAsync(userId);
        return Ok(notifications);
    }

    [HttpPost]
    public async Task<ActionResult<Notification>> CreateNotification([FromBody] CreateNotificationCommand command)
    {
        try
        {
            var notification = await _commandService.Handle(command);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Notification>> UpdateNotification(int id, [FromBody] UpdateNotificationCommand command)
    {
        if (id != command.Id)
            return BadRequest("The ID in the URL does not match the ID in the request body.");

        try
        {
            var updatedNotification = await _commandService.Handle(command);
            return Ok(updatedNotification);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/mark-as-read")]
    public async Task<ActionResult<Notification>> MarkNotificationAsRead(int id)
    {
        try
        {
            var command = new MarkNotificationAsReadCommand(id);
            var notification = await _commandService.Handle(command);
            return Ok(notification);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        try
        {
            var command = new DeleteNotificationCommand(id);
            var result = await _commandService.Handle(command);
            if (result)
                return Ok(new { message = "Notification deleted successfully." });
            else
                return NotFound("Notification not found.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}