using SmartMES.Domain.Enums;

namespace SmartMES.Application.DTOs;

public record AlertDto(int Id, string Message, AlertSeverity Severity, bool IsResolved, DateTime TriggeredAt, DateTime? ResolvedAt, int SensorId, int AlertRuleId);