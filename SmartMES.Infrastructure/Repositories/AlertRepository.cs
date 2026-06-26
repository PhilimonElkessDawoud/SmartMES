using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;
using SmartMES.Infrastructure.Data;

namespace SmartMES.Infrastructure.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _context;

    public AlertRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Alert>> GetAllAsync()
    {
        return await _context.Alerts.ToListAsync();
    }

    public async Task<IEnumerable<Alert>> GetUnresolvedAsync()
    {
        return await _context.Alerts
            .Where(a => !a.IsResolved)
            .ToListAsync();
    }

    public async Task<Alert?> GetByIdAsync(int id)
    {
        return await _context.Alerts.FindAsync(id);
    }

    public async Task AddAsync(Alert alert)
    {
        _context.Alerts.Add(alert);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Alert alert)
    {
        _context.Alerts.Update(alert);
        await _context.SaveChangesAsync();
    }
}