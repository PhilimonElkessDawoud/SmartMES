using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;

namespace SmartMES.Application.Services;

public class SensorAppService : ISensorAppService
{
    private readonly ISensorRepository _repository;

    public SensorAppService(ISensorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SensorDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<SensorDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<IEnumerable<SensorDto>> GetByEquipmentIdAsync(int equipmentId)
    {
        var items = await _repository.GetByEquipmentIdAsync(equipmentId);
        return items.Select(MapToDto);
    }

    public async Task<SensorDto> CreateAsync(CreateSensorDto dto)
    {
        var entity = new Sensor
        {
            Name = dto.Name,
            Type = dto.Type,
            Unit = dto.Unit,
            MinThreshold = dto.MinThreshold,
            MaxThreshold = dto.MaxThreshold,
            EquipmentId = dto.EquipmentId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateSensorDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = dto.Name;
        entity.MinThreshold = dto.MinThreshold;
        entity.MaxThreshold = dto.MaxThreshold;
        entity.IsActive = dto.IsActive;

        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }

    private static SensorDto MapToDto(Sensor entity) =>
        new(entity.Id, entity.Name, entity.Type, entity.Unit, entity.MinThreshold, entity.MaxThreshold, entity.IsActive, entity.EquipmentId);
}