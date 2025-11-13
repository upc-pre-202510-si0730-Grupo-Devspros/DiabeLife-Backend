using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.QueryServices;

internal class GlucoseMeasurementQueryService : IGlucoseMeasurementQueryService
{
    private readonly IGlucoseMeasurementRepository _repository;

    public GlucoseMeasurementQueryService(IGlucoseMeasurementRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GlucoseMeasurement>> Handle()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<GlucoseMeasurement?> Handle(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<GlucoseMeasurement?> GetLatestAsync()
    {
        return await _repository.GetLatestAsync();
    }

    public async Task<IEnumerable<GlucoseMeasurement>> GetRecentAsync(int count = 7)
    {
        return await _repository.GetRecentAsync(count);
    }
}