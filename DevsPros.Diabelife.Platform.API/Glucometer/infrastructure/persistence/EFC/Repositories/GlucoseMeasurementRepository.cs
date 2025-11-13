using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Infrastructure.Persistence.EFC.Repositories;

public class GlucoseMeasurementRepository : IGlucoseMeasurementRepository
{
    private readonly AppDbContext _context;

    public GlucoseMeasurementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GlucoseMeasurement>> GetAllAsync()
    {
        return await _context.GlucoseMeasurements.OrderByDescending(g => g.MeasurementDate).ToListAsync();
    }

    public async Task<GlucoseMeasurement?> GetByIdAsync(int id)
    {
        return await _context.GlucoseMeasurements.FindAsync(id);
    }

    public async Task<GlucoseMeasurement> AddAsync(GlucoseMeasurement glucoseMeasurement)
    {
        await _context.GlucoseMeasurements.AddAsync(glucoseMeasurement);
        await _context.SaveChangesAsync();
        return glucoseMeasurement;
    }

    public async Task<GlucoseMeasurement> UpdateAsync(GlucoseMeasurement glucoseMeasurement)
    {
        _context.GlucoseMeasurements.Update(glucoseMeasurement);
        await _context.SaveChangesAsync();
        return glucoseMeasurement;
    }

    public async Task DeleteAsync(int id)
    {
        var measurement = await _context.GlucoseMeasurements.FindAsync(id);
        if (measurement != null)
        {
            _context.GlucoseMeasurements.Remove(measurement);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<GlucoseMeasurement?> GetLatestAsync()
    {
        return await _context.GlucoseMeasurements
            .OrderByDescending(g => g.MeasurementDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<GlucoseMeasurement>> GetRecentAsync(int count = 7)
    {
        return await _context.GlucoseMeasurements
            .OrderByDescending(g => g.MeasurementDate)
            .Take(count)
            .ToListAsync();
    }
}