namespace DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.CommandServices;

public record CreateNotificationCommand(
    string Title,
    string Message,
    string UserId,
    string Type = "Info"
);

public record UpdateNotificationCommand(
    int Id,
    string Title,
    string Message,
    string Type
);

public record MarkNotificationAsReadCommand(int Id);
public record DeleteNotificationCommand(int Id);