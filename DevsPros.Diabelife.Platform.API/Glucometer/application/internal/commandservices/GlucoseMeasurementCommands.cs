namespace DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.CommandServices;

public record CreateGlucoseMeasurementCommand(double Value, string Unit, string Status, string Trend, DateTime MeasurementDate);
public record UpdateGlucoseMeasurementCommand(int Id, double Value, string Unit, string Status, string Trend, DateTime MeasurementDate);