using SmartMES.Application.DTOs;

namespace SmartMES.Application.Interfaces;

public interface IEquipmentService
{
    Task<IEnumerable<EquipmentDto>> GetAllAsync();
    Task<EquipmentDto?> GetByIdAsync(int id);
    Task<IEnumerable<EquipmentDto>> GetByProductionLineIdAsync(int productionLineId);
    Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto);
    Task<bool> UpdateAsync(int id, UpdateEquipmentDto dto);
    Task<bool> DeleteAsync(int id);
}