using Microsoft.Extensions.Logging;

namespace MultiProject.Delivery.Application.Tests.Integration.Helpers;

public class LoggerMockWrapper<T> : ILogger<T>
{
    private readonly ILogger _logger;

    public LoggerMockWrapper(ILogger logger)
    {
        _logger = logger;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
                            Func<TState, Exception?, string> formatter)
    {
        _logger.Log(logLevel, eventId, state, exception, formatter);
    }

    public bool IsEnabled(LogLevel logLevel) => _logger.IsEnabled(logLevel);

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => _logger.BeginScope(state);
}