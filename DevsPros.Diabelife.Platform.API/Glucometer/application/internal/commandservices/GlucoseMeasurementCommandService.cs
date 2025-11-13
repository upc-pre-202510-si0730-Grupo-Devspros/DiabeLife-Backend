using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Repositories;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.CommandServices;

internal class GlucoseMeasurementCommandService : IGlucoseMeasurementCommandService
{
    private readonly IGlucoseMeasurementRepository _repository;

    public GlucoseMeasurementCommandService(IGlucoseMeasurementRepository repository)
    {
        _repository = repository;
    }

    public async Task<GlucoseMeasurement> Handle(CreateGlucoseMeasurementCommand command)
    {
        var glucoseMeasurement = new GlucoseMeasurement(
            command.Value, command.Unit, command.Status, command.Trend, command.MeasurementDate
        );
        return await _repository.AddAsync(glucoseMeasurement);
    }

    public async Task<GlucoseMeasurement> Handle(UpdateGlucoseMeasurementCommand command)
    {
        var existingMeasurement = await _repository.GetByIdAsync(command.Id);
        if (existingMeasurement == null)
            throw new Exception($"GlucoseMeasurement with id {command.Id} not found");

        existingMeasurement.Update(
            command.Value, command.Unit, command.Status, command.Trend, command.MeasurementDate
        );
        return await _repository.UpdateAsync(existingMeasurement);
    }

    public async Task<bool> Handle(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}