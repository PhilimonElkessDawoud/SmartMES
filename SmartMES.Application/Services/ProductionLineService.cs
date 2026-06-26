using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;

namespace SmartMES.Application.Services;

public class ProductionLineService : IProductionLineService
{
    private readonly IProductionLineRepository _repository;

    public ProductionLineService(IProductionLineRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductionLineDto>> GetAllAsync()
    {
        var lines = await _repository.GetAllAsync();
        return lines.Select(MapToDto);
    }

    public async Task<ProductionLineDto?> GetByIdAsync(int id)
    {
        var line = await _repository.GetByIdAsync(id);
        return line is null ? null : MapToDto(line);
    }

    public async Task<ProductionLineDto> CreateAsync(CreateProductionLineDto dto)
    {
        var entity = new ProductionLine
        {
            Name = dto.Name,
            Location = dto.Location,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateProductionLineDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = dto.Name;
        entity.Location = dto.Location;
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

    private static ProductionLineDto MapToDto(ProductionLine entity) =>
        new(entity.Id, entity.Name, entity.Location, entity.IsActive, entity.CreatedAt);
}