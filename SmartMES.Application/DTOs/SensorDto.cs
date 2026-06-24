using SmartMES.Domain.Enums;

namespace SmartMES.Application.DTOs;

public record SensorDto(int Id, string Name, SensorType Type, string Unit, double MinThreshold, double MaxThreshold, bool IsActive, int EquipmentId);

public record CreateSensorDto(string Name, SensorType Type, string Unit, double MinThreshold, double MaxThreshold, int EquipmentId);

public record UpdateSensorDto(string Name, double MinThreshold, double MaxThreshold, bool IsActive);