using DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST.DTOs;

namespace DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;

public interface IReportCommandService
{
    Task CreateReportAsync(CreateReportDto createDto, int userId);
    Task UpdateReportAsync(int id, UpdateReportDto updateDto, int userId);
    Task DeleteReportAsync(int id, int userId);
    Task ShareReportsAsync(ShareReportsDto shareDto, int userId);
}