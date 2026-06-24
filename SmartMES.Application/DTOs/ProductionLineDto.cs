namespace SmartMES.Application.DTOs;

public record ProductionLineDto(int Id, string Name, string Location, bool IsActive, DateTime CreatedAt);

public record CreateProductionLineDto(string Name, string Location);

public record UpdateProductionLineDto(string Name, string Location, bool IsActive);