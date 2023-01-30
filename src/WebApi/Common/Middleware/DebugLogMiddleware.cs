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
        long startTime = Stopwatch.GetTimestamp();

        //before endpoint
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
        //after endpoint

        _logger.LogDebug("request took: {time}ms", Stopwatch.GetElapsedTime(startTime).Milliseconds);
    }
}
