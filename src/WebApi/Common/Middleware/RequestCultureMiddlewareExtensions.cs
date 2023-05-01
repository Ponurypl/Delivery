namespace MultiProject.Delivery.WebApi.Common.Middleware;

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseTraceLogging(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TraceLogMiddleware>();
    }

    public static IApplicationBuilder UseDebugLogging(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DebugLogMiddleware>();
    }

    public static IApplicationBuilder UseInformationLogging(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CanonicalLogMiddleware>();
    }

}