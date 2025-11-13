using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;

public interface IReportQueryService
{
    Task<IEnumerable<Report>> GetReportsByUserIdAsync(int userId);
    Task<Report?> GetReportByIdAsync(int id, int userId);
}