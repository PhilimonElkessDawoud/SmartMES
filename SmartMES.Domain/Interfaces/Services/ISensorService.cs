using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Services;

public interface ISensorService
{
    Task ProcessReadingAsync(SensorReading reading);
    Task<IEnumerable<SensorReading>> GetReadingsAsync(int sensorId);
    Task<SensorReading?> GetLatestReadingAsync(int sensorId);
}