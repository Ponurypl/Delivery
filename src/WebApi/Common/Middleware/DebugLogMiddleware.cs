using System.Diagnostics;

namespace MultiProject.Delivery.WebApi.Common.Middleware;

public sealed class DebugLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DebugLogMiddleware> _logger;

    public DebugLogMiddleware(RequestDelegate next, ILogger<DebugLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch time = null;
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            time = Stopwatch.StartNew();
        }

        //before endpoint
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
        //after endpoint

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            time?.Stop();
            _logger.LogTrace("request took: {time}ms", time?.ElapsedMilliseconds);
        }
    }
}
