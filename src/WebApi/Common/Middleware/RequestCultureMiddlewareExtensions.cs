namespace MultiProject.Delivery.WebApi.Common.Middleware;

public static class RequestCultureMiddlewareExtensions
{
    //TODO: może to tutaj lub przy budowaniu aplikacji powinniśmy określić jaki poziom logowania trzeba użyć zamiast
    //powoływać każdy i przy każdym request-cie sprawdzać jaki jest poziom i czy dany middleware ma co robić?
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


}