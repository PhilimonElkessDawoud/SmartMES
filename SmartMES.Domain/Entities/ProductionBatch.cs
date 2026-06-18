namespace SmartMES.Domain.Entities;

public class ProductionBatch
{
    public int Id { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public int TargetQuantity { get; set; }
    public int ProducedQuantity { get; set; }
    public int DefectQuantity { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ProductionLineId { get; set; }
    public ProductionLine ProductionLine { get; set; } = null!;
}