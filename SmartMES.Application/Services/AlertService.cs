using SmartMES.Application.DTOs;
using SmartMES.Application.Interfaces;
using SmartMES.Domain.Interfaces.Repositories;

namespace SmartMES.Application.Services;

public class AlertService : IAlertService
{
    private readonly IAlertRepository _repository;

    public AlertService(IAlertRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AlertDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<IEnumerable<AlertDto>> GetUnresolvedAsync()
    {
        var items = await _repository.GetUnresolvedAsync();
        return items.Select(MapToDto);
    }

    public async Task<bool> ResolveAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return false;

        entity.IsResolved = true;
        entity.ResolvedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(entity);
        return true;
    }

    private static AlertDto MapToDto(Domain.Entities.Alert entity) =>
        new(entity.Id, entity.Message, entity.Severity, entity.IsResolved, entity.TriggeredAt, entity.ResolvedAt, entity.SensorId, entity.AlertRuleId);
}