using SmartMES.Application.DTOs;

namespace SmartMES.Application.Interfaces;

public interface IProductionLineService
{
    Task<IEnumerable<ProductionLineDto>> GetAllAsync();
    Task<ProductionLineDto?> GetByIdAsync(int id);
    Task<ProductionLineDto> CreateAsync(CreateProductionLineDto dto);
    Task<bool> UpdateAsync(int id, UpdateProductionLineDto dto);
    Task<bool> DeleteAsync(int id);
}