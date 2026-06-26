using SmartMES.Application.DTOs;

namespace SmartMES.Application.Interfaces;

public interface IAlertRuleService
{
    Task<IEnumerable<AlertRuleDto>> GetAllAsync();
    Task<AlertRuleDto?> GetByIdAsync(int id);
    Task<AlertRuleDto> CreateAsync(CreateAlertRuleDto dto);
    Task<bool> UpdateAsync(int id, UpdateAlertRuleDto dto);
    Task<bool> DeleteAsync(int id);
}