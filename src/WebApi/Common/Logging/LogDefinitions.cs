namespace MultiProject.Delivery.WebApi.Common.Logging;

public partial class LogDefinitions
{
    [LoggerMessage(401, LogLevel.Warning, "Invalid logon attempt. Username used: {username}")]
    public static partial void InvalidLogon(ILogger logger, string username);

    
    
    private const string _requestEntryTraceMessage = """
                                {{
                                    "Request":
                                    {{
                                	        "Headers": "{requestHeaders}",
                                	        "Path": "{requestPath}",
                                            "Query": "{requestQuery}",
                                            "Method": "{requestMethod}",
                                	        "Body": "{requestBody}"
                                    }},
                                    "Response":
                                    {{
                                            "Headers": "{responseHeaders}",
                                            "Body": "{responseBody}",
                                            "StatusCode": {responseStatusCode}
                                    }}
                                }}
                                """;

    [LoggerMessage(0, LogLevel.Trace, _requestEntryTraceMessage)]
    public static partial void RequestEntryTrace(ILogger logger, string requestHeaders, string requestPath,
                                                 string requestQuery, string requestMethod, string requestBody,
                                                 string responseHeaders, string responseBody,
                                                 int responseStatusCode);

    
    private const string _requestCanonicalLogMessage = """
                                {{
                                    "request":
                                    {{
                                        "remoteIp": "{ipAddress}",
                                        "localIP" : "{localIp}"
                                        "userID": "{userId}",
                                        "path": "{requestPath}",
                                        "query": "{query}",
                                        "method": "{requestMethod}",
                                        "traceIdentifier": "{traceIdentifier}"
                                    }},
                                    "response":
                                    {{
                                        "statusCode": {responseStatusCode},
                                        "duration": {requestDuration}
                                    }},
                                    "host":
                                    {{
                                        "name": "{hostName}"
                                    }},
                                    "enviroment":
                                    {{
                                        "machineName": "{machineName}"
                                    }},
                                    application:
                                    {{
                                        "version": "{appVersion}",
                                        "name": "{appName}"
                                    }},
                                    "logLevel": "{logLevel}"
                                }}
                                """;

    [LoggerMessage(299, LogLevel.Information, _requestCanonicalLogMessage)]
    public static partial void RequestCanonicalLog(ILogger logger, string? ipAddress, string? userId,
                                                   string? requestPath,
                                                   string requestMethod, int responseStatusCode, long requestDuration,
                                                   string? localIp, string traceIdentifier, string hostName,
                                                   string machineName, string? appVersion, string? appName, string logLevel, string query);
}