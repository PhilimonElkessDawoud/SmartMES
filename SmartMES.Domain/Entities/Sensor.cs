using SmartMES.Domain.Enums;

namespace SmartMES.Domain.Entities;

public class Sensor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SensorType Type { get; set; }
    public string Unit { get; set; } = string.Empty;
    public double MinThreshold { get; set; }
    public double MaxThreshold { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;

    public ICollection<SensorReading> Readings { get; set; } = new List<SensorReading>();
    public ICollection<AlertRule> AlertRules { get; set; } = new List<AlertRule>();
}