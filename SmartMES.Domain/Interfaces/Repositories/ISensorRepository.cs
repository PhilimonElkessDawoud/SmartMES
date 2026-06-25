using SmartMES.Domain.Entities;

namespace SmartMES.Domain.Interfaces.Repositories;

public interface ISensorRepository
{
    Task<IEnumerable<Sensor>> GetAllAsync();
    Task<Sensor?> GetByIdAsync(int id);
    Task<IEnumerable<Sensor>> GetByEquipmentIdAsync(int equipmentId);
    Task AddAsync(Sensor sensor);
    Task UpdateAsync(Sensor sensor);
    Task DeleteAsync(int id);
}