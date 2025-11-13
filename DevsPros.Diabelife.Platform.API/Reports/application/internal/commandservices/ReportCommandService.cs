using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Reports.Interfaces.REST.DTOs;

namespace DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;

public class ReportCommandService : IReportCommandService
{
    private readonly IReportRepository _reportRepository;

    public ReportCommandService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task CreateReportAsync(CreateReportDto createDto, int userId)
    {
        var report = new Report
        {
            Name = createDto.Name,
            Date = createDto.Date,
            Type = createDto.Type,
            Data = createDto.Data,
            Selected = createDto.Selected,
            Shared = createDto.Shared,
            UserId = userId
        };

        await _reportRepository.AddAsync(report);
        await _reportRepository.SaveChangesAsync();
    }

    public async Task UpdateReportAsync(int id, UpdateReportDto updateDto, int userId)
    {
        var report = await _reportRepository.FindByIdAndUserIdAsync(id, userId);
        if (report == null)
        {
            throw new KeyNotFoundException("Report not found or access denied");
        }

        report.Name = updateDto.Name;
        report.Date = updateDto.Date;
        report.Type = updateDto.Type;
        report.Data = updateDto.Data;
        report.Selected = updateDto.Selected;
        report.Shared = updateDto.Shared;

        _reportRepository.Update(report);
        await _reportRepository.SaveChangesAsync();
    }

    public async Task DeleteReportAsync(int id, int userId)
    {
        var report = await _reportRepository.FindByIdAndUserIdAsync(id, userId);
        if (report == null)
        {
            throw new KeyNotFoundException("Report not found or access denied");
        }

        _reportRepository.Delete(report);
        await _reportRepository.SaveChangesAsync();
    }

    public async Task ShareReportsAsync(ShareReportsDto shareDto, int userId)
    {
        foreach (var reportId in shareDto.ReportIds)
        {
            var report = await _reportRepository.FindByIdAndUserIdAsync(reportId, userId);
            if (report != null)
            {
                report.Shared = shareDto.Shared;
                _reportRepository.Update(report);
            }
        }

        await _reportRepository.SaveChangesAsync();
    }
}