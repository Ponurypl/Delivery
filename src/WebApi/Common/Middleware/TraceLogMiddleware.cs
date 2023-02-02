using MultiProject.Delivery.WebApi.Common.Logging;
using System.Text.Json;

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
        if (!_logger.IsEnabled(LogLevel.Trace))
        {
            await _next(context);
            return;
        }
        
        context.Request.EnableBuffering();

        var responseOriginalStream = context.Response.Body;
        using MemoryStream responseStream = new();
        context.Response.Body = responseStream;

        try
        {
            await _next(context);
        }
        finally
        {
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            using StreamReader requestSr = new(context.Request.Body);
            string requestBody = await requestSr.ReadToEndAsync();
            var requestHeaders = JsonSerializer.Serialize(context.Request.Headers);


            responseStream.Seek(0, SeekOrigin.Begin);
            using StreamReader responseSr = new(responseStream);
            string responseBody = await responseSr.ReadToEndAsync();

            responseStream.Seek(0, SeekOrigin.Begin);
            await responseStream.CopyToAsync(responseOriginalStream);
            context.Response.Body = responseOriginalStream;

            var responseHeaders = JsonSerializer.Serialize(context.Response.Headers);

            LogDefinitions.RequestEntryTrace(_logger, requestHeaders, context.Request.Path,
                                             context.Request.QueryString.Value ?? string.Empty, context.Request.Method,
                                             requestBody, responseHeaders, responseBody, context.Response.StatusCode);
        }
    }
}
