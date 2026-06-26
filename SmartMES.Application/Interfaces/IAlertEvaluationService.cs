using SmartMES.Domain.Entities;

namespace SmartMES.Application.Interfaces;

public interface IAlertEvaluationService
{
    Task EvaluateReadingAsync(SensorReading reading);
}