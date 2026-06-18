using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Repositories;

public interface IAlertRepository
{
    Task<IEnumerable<Alert>> GetAllAsync();
    Task<IEnumerable<Alert>> GetUnresolvedAsync();
    Task<Alert?> GetByIdAsync(int id);
    Task AddAsync(Alert alert);
    Task UpdateAsync(Alert alert);
}