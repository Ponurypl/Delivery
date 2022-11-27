using MultiProject.Delivery.Domain.Common.DateTimeProvider;

namespace MultiProject.Delivery.Infrastructure.DateTimeProvider;

internal sealed class SystemDateTimeProvider : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}