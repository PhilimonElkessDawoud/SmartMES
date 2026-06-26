using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Repositories;

public interface IAlertRuleRepository
{
    Task<IEnumerable<AlertRule>> GetAllAsync();
    Task<AlertRule?> GetByIdAsync(int id);
    Task<IEnumerable<AlertRule>> GetActiveBySensorIdAsync(int sensorId);
    Task AddAsync(AlertRule alertRule);
    Task UpdateAsync(AlertRule alertRule);
    Task DeleteAsync(int id);
}