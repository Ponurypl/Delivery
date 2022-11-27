namespace MultiProject.Delivery.Domain.Common.DateTimeProvider;

public interface IDateTime
{
    DateTime UtcNow { get; }
}
