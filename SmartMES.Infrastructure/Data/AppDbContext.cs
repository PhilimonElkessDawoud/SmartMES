using Microsoft.EntityFrameworkCore;
using SmartMES.Domain.Entities;

namespace SmartMES.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();
    public DbSet<Equipment> Equipments => Set<Equipment>();
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<AlertRule> AlertRules => Set<AlertRule>();
    public DbSet<MaintenanceRecord> MaintenanceRecords => Set<MaintenanceRecord>();
    public DbSet<ProductionBatch> ProductionBatches => Set<ProductionBatch>();
    public DbSet<Shift> Shifts => Set<Shift>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ProductionLine
        modelBuilder.Entity<ProductionLine>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(200);
        });

        // Equipment
        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
            entity.HasOne(e => e.ProductionLine)
                  .WithMany(p => p.Equipments)
                  .HasForeignKey(e => e.ProductionLineId);
        });

        // Sensor
        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(20);
            entity.HasOne(e => e.Equipment)
                  .WithMany(e => e.Sensors)
                  .HasForeignKey(e => e.EquipmentId);
        });

        // SensorReading
        modelBuilder.Entity<SensorReading>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Sensor)
                  .WithMany(s => s.Readings)
                  .HasForeignKey(e => e.SensorId);
        });

        // AlertRule
        modelBuilder.Entity<AlertRule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Condition).IsRequired().HasMaxLength(10);
            entity.HasOne(e => e.Sensor)
                  .WithMany(s => s.AlertRules)
                  .HasForeignKey(e => e.SensorId);
        });

        // Alert
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Message).IsRequired().HasMaxLength(500);
            entity.HasOne(e => e.Sensor)
                  .WithMany()
                  .HasForeignKey(e => e.SensorId)
                  .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.AlertRule)
                  .WithMany()
                  .HasForeignKey(e => e.AlertRuleId)
                  .OnDelete(DeleteBehavior.NoAction);
        });

        // MaintenanceRecord
        modelBuilder.Entity<MaintenanceRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.PerformedBy).HasMaxLength(100);
            entity.HasOne(e => e.Equipment)
                  .WithMany(e => e.MaintenanceRecords)
                  .HasForeignKey(e => e.EquipmentId);
        });

        // ProductionBatch
        modelBuilder.Entity<ProductionBatch>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BatchNumber).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.ProductionLine)
                  .WithMany(p => p.ProductionBatches)
                  .HasForeignKey(e => e.ProductionLineId);
        });

        // Shift
        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Operator).HasMaxLength(100);
            entity.HasOne(e => e.ProductionLine)
                  .WithMany(p => p.Shifts)
                  .HasForeignKey(e => e.ProductionLineId);
        });
    }
}