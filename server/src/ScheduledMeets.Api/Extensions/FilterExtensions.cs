using ScheduledMeets.Internals.Expressions;

using System.Linq.Expressions;

namespace ScheduledMeets.Api.Extensions;

internal static class FilterExtensions
{
    public static Expression<Func<TElement, bool>> Contains<TElement, TValue>(
        this IEnumerable<TValue> values,
        Expression<Func<TElement, TValue>> selector)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(selector);

        return Expression.Lambda<Func<TElement, bool>>(
           AdvancedExpression.Contains(Expression.Constant(values), selector.Body),
           selector.Parameters);
    }
}
