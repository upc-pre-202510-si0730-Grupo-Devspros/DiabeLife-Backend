using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;

public class ReportQueryService : IReportQueryService
{
    private readonly IReportRepository _reportRepository;

    public ReportQueryService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<IEnumerable<Report>> GetReportsByUserIdAsync(int userId)
    {
        return await _reportRepository.ListByUserIdAsync(userId);
    }

    public async Task<Report?> GetReportByIdAsync(int id, int userId)
    {
        return await _reportRepository.FindByIdAndUserIdAsync(id, userId);
    }
}