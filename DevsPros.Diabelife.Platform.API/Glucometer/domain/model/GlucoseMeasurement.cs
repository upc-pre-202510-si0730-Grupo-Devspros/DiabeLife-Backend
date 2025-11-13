using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;

namespace DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;

public class GlucoseMeasurement : BaseEntity
{
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Trend { get; set; } = string.Empty;
    public DateTime MeasurementDate { get; set; }

    public GlucoseMeasurement() { }

    public GlucoseMeasurement(double value, string unit, string status, string trend, DateTime measurementDate)
    {
        Value = value;
        Unit = unit;
        Status = status;
        Trend = trend;
        MeasurementDate = measurementDate;
    }

    public void Update(double value, string unit, string status, string trend, DateTime measurementDate)
    {
        Value = value;
        Unit = unit;
        Status = status;
        Trend = trend;
        MeasurementDate = measurementDate;
        UpdatedAt = DateTime.UtcNow;
    }
}