using Confluent.Kafka;
using System.Text.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

var random = new Random();
var sensorIds = new[] { 1, 2, 3, 4, 5 };

Console.WriteLine("Starting sensor simulator. Press Ctrl+C to stop.");

while (true)
{
    foreach (var sensorId in sensorIds)
    {
        var reading = new
        {
            SensorId = sensorId,
            Value = Math.Round(20 + random.NextDouble() * 60, 2), //  Simulates 20-80 range
            RecordedAt = DateTime.UtcNow
        };

        var message = JsonSerializer.Serialize(reading);

        try
        {
            var result = await producer.ProduceAsync("sensor-readings", new Message<Null, string> { Value = message });
            Console.WriteLine($"Sent: {message} -> {result.TopicPartitionOffset}");
        }
        catch (ProduceException<Null, string> ex)
        {
            Console.WriteLine($"Failed to deliver message: {ex.Error.Reason}");
        }
    }

    await Task.Delay(3000); // Send readings every 3 seconds
}