using FluentValidation;
using System.Linq.Expressions;

namespace MultiProject.Delivery.Domain.Common.Extensions;

public static class FluentValidatotionExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WhenNotEmpty<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Func<T, string?> expression)
    {
        rule.When(x => string.IsNullOrWhiteSpace(expression(x)));
        return rule;
    }
}
