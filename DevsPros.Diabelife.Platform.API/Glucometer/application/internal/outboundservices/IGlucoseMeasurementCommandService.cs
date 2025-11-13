using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;

public interface IGlucoseMeasurementCommandService
{
    Task<GlucoseMeasurement> Handle(CreateGlucoseMeasurementCommand command);
    Task<GlucoseMeasurement> Handle(UpdateGlucoseMeasurementCommand command);
    Task<bool> Handle(int id); // Para Delete
}