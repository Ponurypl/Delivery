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
        Stopwatch time = null;
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            time = Stopwatch.StartNew();
        }
        //before endpoint
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
        //after endpoint
        if (_logger.IsEnabled(LogLevel.Trace))
        {
            time?.Stop();
            _logger.LogTrace("request took: {time}ms", time?.ElapsedMilliseconds);
        }
    }
}
public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseTraceLogging(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TraceLogMiddleware>();
    }
}