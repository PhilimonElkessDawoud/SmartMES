using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Enums;

namespace SmartMES.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.ProductionLines.AnyAsync())
            return; // Already seeded

        var productionLine = new ProductionLine
        {
            Name = "Assembly Line A",
            Location = "Factory Floor 1",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.ProductionLines.Add(productionLine);
        await context.SaveChangesAsync();

        var equipment = new Equipment
        {
            Name = "Conveyor Belt 1",
            Model = "CB-2000",
            SerialNumber = "SN-001",
            Status = EquipmentStatus.Running,
            InstalledAt = DateTime.UtcNow.AddYears(-1),
            CreatedAt = DateTime.UtcNow,
            ProductionLineId = productionLine.Id
        };

        context.Equipments.Add(equipment);
        await context.SaveChangesAsync();

        var sensors = new List<Sensor>
        {
            new() { Name = "Temperature Sensor 1", Type = SensorType.Temperature, Unit = "°C", MinThreshold = 0, MaxThreshold = 80, IsActive = true, CreatedAt = DateTime.UtcNow, EquipmentId = equipment.Id },
            new() { Name = "Pressure Sensor 1", Type = SensorType.Pressure, Unit = "bar", MinThreshold = 0, MaxThreshold = 10, IsActive = true, CreatedAt = DateTime.UtcNow, EquipmentId = equipment.Id },
            new() { Name = "Vibration Sensor 1", Type = SensorType.Vibration, Unit = "mm/s", MinThreshold = 0, MaxThreshold = 50, IsActive = true, CreatedAt = DateTime.UtcNow, EquipmentId = equipment.Id },
            new() { Name = "Humidity Sensor 1", Type = SensorType.Humidity, Unit = "%", MinThreshold = 0, MaxThreshold = 100, IsActive = true, CreatedAt = DateTime.UtcNow, EquipmentId = equipment.Id },
            new() { Name = "Speed Sensor 1", Type = SensorType.Speed, Unit = "m/s", MinThreshold = 0, MaxThreshold = 20, IsActive = true, CreatedAt = DateTime.UtcNow, EquipmentId = equipment.Id }
        };

        context.Sensors.AddRange(sensors);
        await context.SaveChangesAsync();
    }
}