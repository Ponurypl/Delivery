using MultiProject.Delivery.Domain.Common.Abstractions;

namespace MultiProject.Delivery.Application.Tests.Integration.Helpers;

public static class EntityExtensions
{
    public static void SetId<T>(this Entity<T> entity, T value)
    {
        typeof(Entity<T>).GetProperty(nameof(Entity<T>.Id))
                         ?.SetValue(entity, value);
    }
    
}