using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Domain.Repositories;

public interface IGlucoseMeasurementRepository
{
    Task<IEnumerable<GlucoseMeasurement>> GetAllAsync();
    Task<GlucoseMeasurement?> GetByIdAsync(int id);
    Task<GlucoseMeasurement> AddAsync(GlucoseMeasurement glucoseMeasurement);
    Task<GlucoseMeasurement> UpdateAsync(GlucoseMeasurement glucoseMeasurement);
    Task DeleteAsync(int id);
    Task<GlucoseMeasurement?> GetLatestAsync();
    Task<IEnumerable<GlucoseMeasurement>> GetRecentAsync(int count = 7);
}