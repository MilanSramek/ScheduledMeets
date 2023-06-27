using System.Linq.Expressions;
using ScheduledMeets.Internals.Reflection;

namespace ScheduledMeets.Internals.Expressions;

public static class AdvancedExpression
{
    public static MethodCallExpression Contains(Expression sequence, Expression element)
    {
        ArgumentNullException.ThrowIfNull(sequence);
        ArgumentNullException.ThrowIfNull(element);

        return Expression.Call(null, Method.Contains(element.Type), sequence, element);
    }
}
