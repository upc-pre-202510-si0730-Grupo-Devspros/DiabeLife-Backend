using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;

public interface IReportRepository
{
    Task<IEnumerable<Report>> ListByUserIdAsync(int userId);
    Task<Report?> FindByIdAndUserIdAsync(int id, int userId);
    Task<Report?> FindByIdAsync(int id);
    Task AddAsync(Report report);
    void Update(Report report);
    void Delete(Report report);
    Task SaveChangesAsync();
}