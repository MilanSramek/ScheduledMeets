using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;

namespace ScheduledMeets.Internals.Expressions;

public static class ExpressionComparer
{
    public static IEqualityComparer<Expression> Instance => ExpressionEqualityComparer.Instance;
}
