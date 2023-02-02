namespace MultiProject.Delivery.WebApi.Common.Logging;

public partial class LogDefinitions
{
    [LoggerMessage(0, LogLevel.Warning, "Invalid logon attempt. Username used: {username}")]
    public static partial void InvalidLogon(ILogger logger, string username);

    
    
    private const string requestEntryTraceMessage = """
                                Request:
                                	Headers: {requestHeaders}
                                	Path: {requestPath}
                                    Query: {requestQuery}
                                    Method: {requestMethod}
                                	Body: {requestBody}

                                Response:
                                    Headers: {responseHeaders}
                                    Body: {responseBody}
                                    StatusCode: {responseStatusCode}
                                """;

    [LoggerMessage(0, LogLevel.Trace, requestEntryTraceMessage)]
    public static partial void RequestEntryTrace(ILogger logger, string requestHeaders, string requestPath,
                                                 string requestQuery, string requestMethod, string requestBody,
                                                 string responseHeaders, string responseBody,
                                                 int responseStatusCode);
}