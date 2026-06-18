namespace SmartMES.Domain.Entities;

public class Shift
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g. "Morning", "Afternoon", "Night"
    public string Operator { get; set; } = string.Empty;
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ProductionLineId { get; set; }
    public ProductionLine ProductionLine { get; set; } = null!;
}