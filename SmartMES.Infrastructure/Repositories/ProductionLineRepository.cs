using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;
using SmartMES.Infrastructure.Data;

namespace SmartMES.Infrastructure.Repositories;

public class ProductionLineRepository : IProductionLineRepository
{
    private readonly AppDbContext _context;

    public ProductionLineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductionLine>> GetAllAsync()
    {
        return await _context.ProductionLines.ToListAsync();
    }

    public async Task<ProductionLine?> GetByIdAsync(int id)
    {
        return await _context.ProductionLines.FindAsync(id);
    }

    public async Task AddAsync(ProductionLine productionLine)
    {
        _context.ProductionLines.Add(productionLine);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductionLine productionLine)
    {
        _context.ProductionLines.Update(productionLine);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.ProductionLines.FindAsync(id);
        if (entity is not null)
        {
            _context.ProductionLines.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}