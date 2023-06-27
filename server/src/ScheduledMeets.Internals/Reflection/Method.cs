using System.Linq.Expressions;
using System.Reflection;

namespace ScheduledMeets.Internals.Reflection;

internal static class Method
{
    private static readonly Lazy<MethodInfo> _contains = new(GetInfoFactory(_ => _.Contains(0)),
        LazyThreadSafetyMode.PublicationOnly);

    public static MethodInfo Contains(Type genericArgument) => _contains.Value
        .MakeGenericMethod(genericArgument);

    private static Func<MethodInfo> GetInfoFactory<TResult>(Expression<Func<IEnumerable<int>, TResult>> exp) => ()
        => GetInfo(exp);

    private static MethodInfo GetInfo<TResult>(Expression<Func<IEnumerable<int>, TResult>> exp) =>
        ((MethodCallExpression)exp.Body).Method.GetGenericMethodDefinition();
}
