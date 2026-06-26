using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;
using SmartMES.Infrastructure.Data;

namespace SmartMES.Infrastructure.Repositories;

public class AlertRuleRepository : IAlertRuleRepository
{
    private readonly AppDbContext _context;

    public AlertRuleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AlertRule>> GetAllAsync()
    {
        return await _context.AlertRules.ToListAsync();
    }

    public async Task<AlertRule?> GetByIdAsync(int id)
    {
        return await _context.AlertRules.FindAsync(id);
    }

    public async Task<IEnumerable<AlertRule>> GetActiveBySensorIdAsync(int sensorId)
    {
        return await _context.AlertRules
            .Where(r => r.SensorId == sensorId && r.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(AlertRule alertRule)
    {
        _context.AlertRules.Add(alertRule);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AlertRule alertRule)
    {
        _context.AlertRules.Update(alertRule);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.AlertRules.FindAsync(id);
        if (entity is not null)
        {
            _context.AlertRules.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}