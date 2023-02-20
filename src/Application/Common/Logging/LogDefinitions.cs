using Microsoft.Extensions.Logging;

namespace MultiProject.Delivery.Application.Common.Logging;
public partial class LogDefinitions
{
    private const string _validationFailuresMessage = """
                            Validation failures occured on application level while proccesing the request.
                                Message type:
                                    {requestMessage}
                                Request:
                                    {request}
                                Failures:
                                    {failures}
                            """;

    private const string _requestProcessingTrace = """
                                Message type:
                                    {requestMessage}
                                Request:
                                    {request}
                                Response:
                                    {response}
                                """;

    [LoggerMessage(400, LogLevel.Warning, _validationFailuresMessage)]
    public static partial void ValidationFailures(ILogger logger, string requestMessage,  string request, string failures);

    [LoggerMessage(500, LogLevel.Critical, "Unhandled exception encountered:\n")]
    public static partial void UnhandledException(ILogger logger, Exception exception);

    [LoggerMessage(0, LogLevel.Trace, _requestProcessingTrace)]
    public static partial void RequestProcessingTrace(ILogger logger, string requestMessage, 
                                                      string request,  string response);
}
