namespace SmartMES.Domain.Entities;

public class MaintenanceRecord
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PerformedBy { get; set; } = string.Empty;
    public DateTime PerformedAt { get; set; }
    public DateTime? NextScheduledAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;
}