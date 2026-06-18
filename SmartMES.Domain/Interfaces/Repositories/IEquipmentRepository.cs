using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Repositories;

public interface IEquipmentRepository
{
    Task<IEnumerable<Equipment>> GetAllAsync();
    Task<Equipment?> GetByIdAsync(int id);
    Task<IEnumerable<Equipment>> GetByProductionLineIdAsync(int productionLineId);
    Task AddAsync(Equipment equipment);
    Task UpdateAsync(Equipment equipment);
}