using System.Diagnostics;

namespace MultiProject.Delivery.WebApi.Common.Middleware;

public sealed class TraceLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TraceLogMiddleware> _logger;

    public TraceLogMiddleware(RequestDelegate next, ILogger<TraceLogMiddleware> logger)
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

        _logger.LogTrace("request took: {time}ms", Stopwatch.GetElapsedTime(startTime).Milliseconds);
    }
}
