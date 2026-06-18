namespace SmartMES.Domain.Entities;

public class SensorReading
{
    public int Id { get; set; }
    public double Value { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

    public int SensorId { get; set; }
    public Sensor Sensor { get; set; } = null!;
}