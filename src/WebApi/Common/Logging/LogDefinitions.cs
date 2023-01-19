namespace MultiProject.Delivery.WebApi.Common.Logging;

public partial class LogDefinitions
{
    [LoggerMessage(0, LogLevel.Warning, "Invalid logon attempt. Username used: {username}")]
    public static partial void InvalidLogon(ILogger logger, string username);
}