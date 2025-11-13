using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(string email);
    Task AddAsync(User user);
    Task<User?> FindByIdAsync(int id);
    Task SaveChangesAsync();
}