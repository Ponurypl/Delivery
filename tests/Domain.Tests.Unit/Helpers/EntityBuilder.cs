using MultiProject.Delivery.Domain.Common.Abstractions;
using System.Reflection;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public static class EntityBuilder
{
    public static T Create<T, TId>(TId id) where T : Entity<TId> where TId : notnull
    {
        return (Activator.CreateInstance(typeof(T), BindingFlags.NonPublic | BindingFlags.Instance, null, new object?[] { id }, null) as T)!;
    }
}