using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;

public class Notification : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Info"; // Info, Warning, Error, Success
    public string UserId { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;

    public Notification() { }

    public Notification(string title, string message, string userId, string type)
    {
        Title = title;
        Message = message;
        UserId = userId;
        Type = type;
        IsRead = false;
    }

    public void UpdateNotificationData(string title, string message, string type)
    {
        Title = title;
        Message = message;
        Type = type;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        IsRead = true;
        UpdatedAt = DateTime.UtcNow;
    }
}