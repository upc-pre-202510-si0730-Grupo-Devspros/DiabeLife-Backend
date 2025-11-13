using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;

public interface IGlucoseMeasurementQueryService
{
    Task<IEnumerable<GlucoseMeasurement>> Handle();
    Task<GlucoseMeasurement?> Handle(int id);
    Task<GlucoseMeasurement?> GetLatestAsync();
    Task<IEnumerable<GlucoseMeasurement>> GetRecentAsync(int count = 7);
}