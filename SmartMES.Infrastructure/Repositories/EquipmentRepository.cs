using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;
using SmartMES.Infrastructure.Data;

namespace SmartMES.Infrastructure.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly AppDbContext _context;

    public EquipmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipment>> GetAllAsync()
    {
        return await _context.Equipments.ToListAsync();
    }

    public async Task<Equipment?> GetByIdAsync(int id)
    {
        return await _context.Equipments.FindAsync(id);
    }

    public async Task<IEnumerable<Equipment>> GetByProductionLineIdAsync(int productionLineId)
    {
        return await _context.Equipments
            .Where(e => e.ProductionLineId == productionLineId)
            .ToListAsync();
    }

    public async Task AddAsync(Equipment equipment)
    {
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Equipment equipment)
    {
        _context.Equipments.Update(equipment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Equipments.FindAsync(id);
        if (entity is not null)
        {
            _context.Equipments.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}