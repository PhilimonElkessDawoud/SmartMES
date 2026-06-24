using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartMES.Domain.Entities;
using SmartMES.Infrastructure.Data;
using System.Text.Json;

namespace SmartMES.Infrastructure.Kafka.Consumer;

public class SensorReadingConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SensorReadingConsumer> _logger;
    private readonly IConsumer<Null, string> _consumer;

    public SensorReadingConsumer(IServiceScopeFactory scopeFactory, ILogger<SensorReadingConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "smartmes-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("sensor-readings");
        _logger.LogInformation("Kafka consumer started, listening on 'sensor-readings'");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                if (result?.Message?.Value is null)
                    continue;

                var data = JsonSerializer.Deserialize<SensorReadingMessage>(result.Message.Value);

                if (data is null)
                    continue;

                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var sensorExists = await dbContext.Sensors.AnyAsync(s => s.Id == data.SensorId, stoppingToken);

                if (!sensorExists)
                {
                    _logger.LogWarning("Sensor with Id {SensorId} does not exist. Skipping reading.", data.SensorId);
                    continue;
                }

                var reading = new SensorReading
                {
                    SensorId = data.SensorId,
                    Value = data.Value,
                    RecordedAt = data.RecordedAt
                };

                dbContext.SensorReadings.Add(reading);
                await dbContext.SaveChangesAsync(stoppingToken);

                _logger.LogInformation("Saved reading: SensorId={SensorId}, Value={Value}", data.SensorId, data.Value);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Kafka message");
            }
        }

        _consumer.Close();
    }

    private record SensorReadingMessage(int SensorId, double Value, DateTime RecordedAt);
}