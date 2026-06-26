using SmartMES.Domain.Enums;

namespace SmartMES.Application.DTOs;

public record AlertRuleDto(int Id, string Name, double ThresholdValue, string Condition, AlertSeverity Severity, bool IsActive, int SensorId);

public record CreateAlertRuleDto(string Name, double ThresholdValue, string Condition, AlertSeverity Severity, int SensorId);

public record UpdateAlertRuleDto(string Name, double ThresholdValue, string Condition, AlertSeverity Severity, bool IsActive);