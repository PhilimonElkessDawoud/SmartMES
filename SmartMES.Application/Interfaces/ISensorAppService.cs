using SmartMES.Application.DTOs;

namespace SmartMES.Application.Interfaces;

public interface ISensorAppService
{
    Task<IEnumerable<SensorDto>> GetAllAsync();
    Task<SensorDto?> GetByIdAsync(int id);
    Task<IEnumerable<SensorDto>> GetByEquipmentIdAsync(int equipmentId);
    Task<SensorDto> CreateAsync(CreateSensorDto dto);
    Task<bool> UpdateAsync(int id, UpdateSensorDto dto);
    Task<bool> DeleteAsync(int id);
}