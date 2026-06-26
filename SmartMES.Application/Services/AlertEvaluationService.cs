using Microsoft.Extensions.Logging;
using SmartMES.Application.Interfaces;
using SmartMES.Domain.Entities;
using SmartMES.Domain.Interfaces.Repositories;

namespace SmartMES.Application.Services;

public class AlertEvaluationService : IAlertEvaluationService
{
    private readonly IAlertRuleRepository _alertRuleRepository;
    private readonly IAlertRepository _alertRepository;
    private readonly ILogger<AlertEvaluationService> _logger;

    public AlertEvaluationService(
        IAlertRuleRepository alertRuleRepository,
        IAlertRepository alertRepository,
        ILogger<AlertEvaluationService> logger)
    {
        _alertRuleRepository = alertRuleRepository;
        _alertRepository = alertRepository;
        _logger = logger;
    }

    public async Task EvaluateReadingAsync(SensorReading reading)
    {
        var rules = await _alertRuleRepository.GetActiveBySensorIdAsync(reading.SensorId);

        foreach (var rule in rules)
        {
            if (IsTriggered(reading.Value, rule.ThresholdValue, rule.Condition))
            {
                var alert = new Alert
                {
                    Message = $"Sensor {reading.SensorId} reading ({reading.Value}) {rule.Condition} threshold ({rule.ThresholdValue}) — rule '{rule.Name}' triggered.",
                    Severity = rule.Severity,
                    IsResolved = false,
                    TriggeredAt = DateTime.UtcNow,
                    SensorId = reading.SensorId,
                    AlertRuleId = rule.Id
                };

                await _alertRepository.AddAsync(alert);

                _logger.LogWarning("ALERT TRIGGERED: {Message}", alert.Message);
            }
        }
    }

    private static bool IsTriggered(double value, double threshold, string condition) => condition switch
    {
        ">" => value > threshold,
        "<" => value < threshold,
        ">=" => value >= threshold,
        "<=" => value <= threshold,
        "==" => Math.Abs(value - threshold) < 0.0001,
        _ => false
    };
}