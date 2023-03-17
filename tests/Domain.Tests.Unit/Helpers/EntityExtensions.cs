using System.Linq.Expressions;
using System.Reflection;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public static class EntityExtensions
{
    public static void Set<TEntity, TProperty>(this TEntity entity, Expression<Func<TEntity, TProperty>> expression, TProperty value) 
        where TEntity : class
    {
        MemberExpression? body = expression.Body as MemberExpression;

        if (body is null)
        {
            UnaryExpression ubody = (UnaryExpression)expression.Body;
            body = ubody.Operand as MemberExpression;
        }

        if (body is null) throw new ArgumentException("Invalid expression");
        
        typeof(TEntity).GetProperty(body.Member.Name).SetValue(entity, value);
    }
    
}