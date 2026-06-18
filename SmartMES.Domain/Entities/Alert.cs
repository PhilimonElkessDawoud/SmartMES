using SmartMES.Domain.Enums;

namespace SmartMES.Domain.Entities;

public class Alert
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public AlertSeverity Severity { get; set; }
    public bool IsResolved { get; set; } = false;
    public DateTime TriggeredAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }

    public int SensorId { get; set; }
    public Sensor Sensor { get; set; } = null!;

    public int AlertRuleId { get; set; }
    public AlertRule AlertRule { get; set; } = null!;
}