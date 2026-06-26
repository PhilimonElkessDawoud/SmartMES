using Microsoft.EntityFrameworkCore;
using SmartMES.Application.Interfaces;
using SmartMES.Application.Services;
using SmartMES.Domain.Interfaces.Repositories;
using SmartMES.Infrastructure.Data;
using SmartMES.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();

// Services
builder.Services.AddScoped<IProductionLineService, ProductionLineService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<ISensorAppService, SensorAppService>();

// Kafka Consumer (background service)
builder.Services.AddHostedService<SmartMES.Infrastructure.Kafka.Consumer.SensorReadingConsumer>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.SeedAsync(dbContext);
}

app.Run();