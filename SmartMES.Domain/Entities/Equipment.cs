using SmartMES.Domain.Enums;

namespace SmartMES.Domain.Entities;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Idle;
    public DateTime InstalledAt { get; set; }
    public DateTime? LastMaintenanceAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ProductionLineId { get; set; }
    public ProductionLine ProductionLine { get; set; } = null!;

    public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
}