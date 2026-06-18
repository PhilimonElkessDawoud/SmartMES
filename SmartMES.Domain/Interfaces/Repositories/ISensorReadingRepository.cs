using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Repositories;

public interface ISensorReadingRepository
{
    Task<IEnumerable<SensorReading>> GetBySensorIdAsync(int sensorId);
    Task<SensorReading?> GetLatestBySensorIdAsync(int sensorId);
    Task AddAsync(SensorReading reading);
}