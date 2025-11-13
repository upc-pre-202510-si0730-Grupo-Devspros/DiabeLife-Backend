using DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;
using DevsPros.Diabelife.Platform.API.Notifications.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.CommandServices;

public class NotificationCommandService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationCommandService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<Notification> Handle(CreateNotificationCommand command)
    {
        var notification = new Notification(command.Title, command.Message, command.UserId, command.Type);
        return await _notificationRepository.AddAsync(notification);
    }

    public async Task<Notification> Handle(UpdateNotificationCommand command)
    {
        var existingNotification = await _notificationRepository.GetByIdAsync(command.Id);
        if (existingNotification == null)
            throw new Exception($"Notification with id {command.Id} not found");
        
        existingNotification.UpdateNotificationData(command.Title, command.Message, command.Type);
        return await _notificationRepository.UpdateAsync(existingNotification);
    }

    public async Task<Notification> Handle(MarkNotificationAsReadCommand command)
    {
        var existingNotification = await _notificationRepository.GetByIdAsync(command.Id);
        if (existingNotification == null)
            throw new Exception($"Notification with id {command.Id} not found");
        
        existingNotification.MarkAsRead();
        return await _notificationRepository.UpdateAsync(existingNotification);
    }

    public async Task<bool> Handle(DeleteNotificationCommand command)
    {
        try
        {
            await _notificationRepository.DeleteAsync(command.Id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}