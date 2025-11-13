using DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;
using DevsPros.Diabelife.Platform.API.Notifications.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.QueryServices;

public class NotificationQueryService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationQueryService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<IEnumerable<Notification>> Handle()
    {
        return await _notificationRepository.GetAllAsync();
    }

    public async Task<Notification?> Handle(int id)
    {
        return await _notificationRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(string userId)
    {
        return await _notificationRepository.GetByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Notification>> GetByUserIdAndTypeAsync(string userId, string type)
    {
        return await _notificationRepository.GetByUserIdAndTypeAsync(userId, type);
    }
}