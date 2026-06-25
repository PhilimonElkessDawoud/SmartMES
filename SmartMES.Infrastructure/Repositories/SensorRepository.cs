using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;
using SmartMES.Infrastructure.Data;

namespace SmartMES.Infrastructure.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly AppDbContext _context;

    public SensorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sensor>> GetAllAsync()
    {
        return await _context.Sensors.ToListAsync();
    }

    public async Task<Sensor?> GetByIdAsync(int id)
    {
        return await _context.Sensors.FindAsync(id);
    }

    public async Task<IEnumerable<Sensor>> GetByEquipmentIdAsync(int equipmentId)
    {
        return await _context.Sensors
            .Where(s => s.EquipmentId == equipmentId)
            .ToListAsync();
    }

    public async Task AddAsync(Sensor sensor)
    {
        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sensor sensor)
    {
        _context.Sensors.Update(sensor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Sensors.FindAsync(id);
        if (entity is not null)
        {
            _context.Sensors.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}