using SmartMES.Domain.Enums;

namespace SmartMES.Domain.Entities;

public class AlertRule
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double ThresholdValue { get; set; }
    public string Condition { get; set; } = string.Empty; // ">" | "<" | "==" | ">=" | "<="
    public AlertSeverity Severity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int SensorId { get; set; }
    public Sensor Sensor { get; set; } = null!;
}