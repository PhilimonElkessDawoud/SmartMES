using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;

namespace SmartMES.Application.Services;

public class AlertRuleService : IAlertRuleService
{
    private readonly IAlertRuleRepository _repository;

    private static readonly HashSet<string> ValidConditions = new() { ">", "<", ">=", "<=", "==" };

    public AlertRuleService(IAlertRuleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AlertRuleDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<AlertRuleDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item is null ? null : MapToDto(item);
    }

    public async Task<AlertRuleDto> CreateAsync(CreateAlertRuleDto dto)
    {
        if (!ValidConditions.Contains(dto.Condition))
            throw new ArgumentException($"Invalid condition '{dto.Condition}'. Must be one of: {string.Join(", ", ValidConditions)}");

        var entity = new AlertRule
        {
            Name = dto.Name,
            ThresholdValue = dto.ThresholdValue,
            Condition = dto.Condition,
            Severity = dto.Severity,
            SensorId = dto.SensorId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateAlertRuleDto dto)
    {
        if (!ValidConditions.Contains(dto.Condition))
            throw new ArgumentException($"Invalid condition '{dto.Condition}'. Must be one of: {string.Join(", ", ValidConditions)}");

        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.Name = dto.Name;
        entity.ThresholdValue = dto.ThresholdValue;
        entity.Condition = dto.Condition;
        entity.Severity = dto.Severity;
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

    private static AlertRuleDto MapToDto(AlertRule entity) =>
        new(entity.Id, entity.Name, entity.ThresholdValue, entity.Condition, entity.Severity, entity.IsActive, entity.SensorId);
}