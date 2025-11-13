using DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Notifications.Domain.Repositories;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification?> GetByIdAsync(int id);
    Task<Notification> AddAsync(Notification notification);
    Task<Notification> UpdateAsync(Notification notification);
    Task DeleteAsync(int id);
    Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
    Task<IEnumerable<Notification>> GetByUserIdAndTypeAsync(string userId, string type);
}