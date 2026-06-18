namespace SmartMES.Domain.Entities;

public class ProductionLine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    public ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}