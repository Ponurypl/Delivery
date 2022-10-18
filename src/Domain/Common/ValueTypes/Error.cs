namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public sealed record Error(string Code, string Message)
{
    internal static Error None => new Error(string.Empty, string.Empty); 
}
