using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Repositories;

public interface IProductionLineRepository
{
    Task<IEnumerable<ProductionLine>> GetAllAsync();
    Task<ProductionLine?> GetByIdAsync(int id);
    Task AddAsync(ProductionLine productionLine);
    Task UpdateAsync(ProductionLine productionLine);
    Task DeleteAsync(int id);
}