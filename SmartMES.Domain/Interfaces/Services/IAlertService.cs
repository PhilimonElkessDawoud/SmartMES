using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Services;

public interface IAlertService
{
    Task EvaluateReadingAsync(SensorReading reading);
    Task ResolveAlertAsync(int alertId);
    Task<IEnumerable<Alert>> GetActiveAlertsAsync();
}