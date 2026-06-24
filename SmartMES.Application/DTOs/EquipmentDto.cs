using SmartMES.Domain.Enums;

namespace SmartMES.Application.DTOs;

public record EquipmentDto(int Id, string Name, string Model, string SerialNumber, EquipmentStatus Status, DateTime InstalledAt, int ProductionLineId);

public record CreateEquipmentDto(string Name, string Model, string SerialNumber, DateTime InstalledAt, int ProductionLineId);

public record UpdateEquipmentDto(string Name, string Model, string SerialNumber, EquipmentStatus Status);