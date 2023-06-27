using System.Linq.Expressions;

namespace ScheduledMeets.View;

public interface IByFilterReader<TView>
{
    Task<IReadOnlyList<TView>> WithAsync<TValue>(
        Expression<Func<TView, TValue>> selector,
        TValue value,
        CancellationToken cancellationToken) where TValue : notnull;

    Task<IReadOnlyList<IReadOnlyList<TView>>> WithAsync<TValue>(
        Expression<Func<TView, TValue>> selector,
        IReadOnlyCollection<TValue> values,
        CancellationToken cancellationToken) where TValue : notnull;
}
