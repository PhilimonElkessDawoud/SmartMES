using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;

namespace SmartMES.Application.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IEquipmentRepository _repository;

    public EquipmentService(IEquipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EquipmentDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<EquipmentDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<IEnumerable<EquipmentDto>> GetByProductionLineIdAsync(int productionLineId)
    {
        var items = await _repository.GetByProductionLineIdAsync(productionLineId);
        return items.Select(MapToDto);
    }

    public async Task<EquipmentDto> CreateAsync(CreateEquipmentDto dto)
    {
        var entity = new Equipment
        {
            Name = dto.Name,
            Model = dto.Model,
            SerialNumber = dto.SerialNumber,
            InstalledAt = dto.InstalledAt,
            ProductionLineId = dto.ProductionLineId,
            Status = Domain.Enums.EquipmentStatus.Idle,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateEquipmentDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = dto.Name;
        entity.Model = dto.Model;
        entity.SerialNumber = dto.SerialNumber;
        entity.Status = dto.Status;

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

    private static EquipmentDto MapToDto(Equipment entity) =>
        new(entity.Id, entity.Name, entity.Model, entity.SerialNumber, entity.Status, entity.InstalledAt, entity.ProductionLineId);
}