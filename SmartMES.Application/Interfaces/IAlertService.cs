using SmartMES.Application.DTOs;

namespace SmartMES.Application.Interfaces;

public interface IAlertService
{
    Task<IEnumerable<AlertDto>> GetAllAsync();
    Task<IEnumerable<AlertDto>> GetUnresolvedAsync();
    Task<bool> ResolveAsync(int id);
}