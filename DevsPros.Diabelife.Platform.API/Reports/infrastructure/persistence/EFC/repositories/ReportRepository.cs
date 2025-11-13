using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Reports.Infrastructure.Persistence.EFC.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Report>> ListByUserIdAsync(int userId)
    {
        return await _context.Reports
            .Where(r => r.UserId == userId)
            .Include(r => r.User)
            .ToListAsync();
    }

    public async Task<Report?> FindByIdAndUserIdAsync(int id, int userId)
    {
        return await _context.Reports
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id && r.UserId == userId);
    }

    public async Task<Report?> FindByIdAsync(int id)
    {
        return await _context.Reports
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
    }

    public void Update(Report report)
    {
        _context.Reports.Update(report);
    }

    public void Delete(Report report)
    {
        _context.Reports.Remove(report);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}