using MultiProject.Delivery.Domain.Common.Abstractions;
using System.Reflection;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public static class EntityBuilder
{
    public static T Create<T, TId>(TId id) where T : Entity<TId> where TId : notnull
    {
        return (Activator.CreateInstance(typeof(T), BindingFlags.NonPublic | BindingFlags.Instance, null, new object?[] { id }, null) as T)!;
    }

    public static T Create<T, TId>(Func<TId> factory) where T : Entity<TId> where TId : notnull
    {
        if (factory == null) throw new ArgumentNullException(nameof(factory));
        return Create<T, TId>(factory.Invoke());
    }
}