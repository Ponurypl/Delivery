using Microsoft.Extensions.Logging;

namespace MultiProject.Delivery.Application.Common.Logging;
public partial class LogDefinitions
{
    private const string validationFailuresMessage = """
                            Validation Failures occured on Application level while proccesing the request.
                                Method:
                                    {method}
                                Request:
                                    {request}
                                Failures:
                                    {failures}
                            """;

    [LoggerMessage(400, LogLevel.Warning, validationFailuresMessage)]
    public static partial void ValidationFailures(ILogger logger, string method,  string request, string failures);

    [LoggerMessage(500, LogLevel.Critical, "Unhandled exception encountered:\n")]
    public static partial void UnhandledException(ILogger logger, Exception exception);

    private const string requestProcessingTrace = """
                                Method:
                                    {requestMethod}
                                Request:
                                    {request}
                                Response:
                                    {response}
                                """;

    [LoggerMessage(0, LogLevel.Trace, requestProcessingTrace)]
    public static partial void RequestProcessingTrace(ILogger logger, string requestMethod, 
                                                      string request,  string response);
}
