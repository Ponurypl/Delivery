using MultiProject.Delivery.WebApi.Common.Logging;
using System.Diagnostics;
using System.Reflection;

namespace MultiProject.Delivery.WebApi.Common.Middleware;

public sealed class CanonicalLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CanonicalLogMiddleware> _logger;

    public CanonicalLogMiddleware(RequestDelegate next, ILogger<CanonicalLogMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!_logger.IsEnabled(LogLevel.Information))
        {
            await _next(context);
            return;
        }
        
        long startTime = Stopwatch.GetTimestamp();

        //before endpoint
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
        //after endpoint

        long endTime = Stopwatch.GetElapsedTime(startTime).Milliseconds;
        string? ipAddress = context.Connection.RemoteIpAddress?.ToString();
        string? localIp = context.Connection.LocalIpAddress?.ToString();
        string traceIdentifier = context.TraceIdentifier;
        string hostName = System.Net.Dns.GetHostName();
        string envMachineName = Environment.MachineName;
        string? appVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        string? appName = Assembly.GetExecutingAssembly().GetName().Name;
        string logLevel = LogLevel.Information.ToString();
        string? userId = context.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        string query = context.Request.QueryString.ToString();

        LogDefinitions.RequestCanonicalLog(_logger, ipAddress, userId, context.Request.Path.Value,
                                           context.Request.Method, context.Response.StatusCode, endTime, localIp,
                                           traceIdentifier, hostName, envMachineName, appVersion, appName, logLevel, query);
    }
}
