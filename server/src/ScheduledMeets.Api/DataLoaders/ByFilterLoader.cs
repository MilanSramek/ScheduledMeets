using HotChocolate.Types.Relay;

using ScheduledMeets.Api.Extensions;
using ScheduledMeets.Internals.Extensions;
using ScheduledMeets.View;

using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ScheduledMeets.Api.DataLoaders;

internal sealed class ByFilterLoader<TSource, TValue> : GroupedDataLoader<TValue, TSource>
    where TValue : notnull
{
    private readonly Expression<Func<TSource, TValue>> _keySelector;
    private readonly IReader<TSource> _reader;

    public ByFilterLoader(
        Expression<Func<TSource, TValue>> keySelector,
        IReader<TSource> reader,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector));
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    }

    protected override async Task<ILookup<TValue, TSource>> LoadGroupedBatchAsync(
        IReadOnlyList<TValue> keys,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(keys);
        return await _reader
            .Where(keys.Contains(_keySelector))
            .ToLookupAsync(_keySelector, cancellationToken);
    }
}

internal sealed class ByFilterLoader<TSource> : IByFilterReader<TSource>, IDisposable
{
    private readonly IReader<TSource> _reader;
    private readonly IBatchScheduler _batchScheduler;
    private readonly DataLoaderOptions? _options;

    private readonly ConcurrentDictionary<MemberInfo, IDataLoader> _loaders = new();
    private bool _isDisposed = false;

    public ByFilterLoader(
        IReader<TSource> reader,
        IBatchScheduler batchScheduler,
        DataLoaderOptions? options)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        _batchScheduler = batchScheduler ?? throw new ArgumentNullException(nameof(batchScheduler));
        _options = options;
    }

    public async Task<IReadOnlyList<TSource>> WithAsync<TValue>(
        Expression<Func<TSource, TValue>> selector,
        TValue value,
        CancellationToken cancellationToken) where TValue : notnull
    {
        ArgumentNullException.ThrowIfNull(selector);
        if (selector.Body is not MemberExpression { Expression: ParameterExpression })
            throw new ArgumentException("Simple member selector expected.", nameof(selector));

        if (_isDisposed) throw new InvalidOperationException("Loader has been disposed.");

        ByFilterLoader<TSource, TValue> loader = GetLoader(selector);
        return await loader.LoadAsync(value, cancellationToken);
    }

    public async Task<IReadOnlyList<IReadOnlyList<TSource>>> WithAsync<TValue>(
        Expression<Func<TSource, TValue>> selector,
        IReadOnlyCollection<TValue> values,
        CancellationToken cancellationToken) where TValue : notnull
    {
        ArgumentNullException.ThrowIfNull(selector);
        if (selector.Body is not MemberExpression { Expression: ParameterExpression })
            throw new ArgumentException("Simple member selector expected.", nameof(selector));

        if (_isDisposed) throw new InvalidOperationException("Loader has been disposed.");

        ByFilterLoader<TSource, TValue> loader = GetLoader(selector);
        return await loader.LoadAsync(values, cancellationToken);
    }

    private ByFilterLoader<TSource, TValue> GetLoader<TValue>(
        Expression<Func<TSource, TValue>> selector) where TValue : notnull
    {
        static ByFilterLoader<TSource, TValue> CreateLoader((
            Expression<Func<TSource, TValue>> selector,
            IReader<TSource> reader,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options) _)
        {
            return new ByFilterLoader<TSource, TValue>(
                _.selector,
                _.reader,
                _.batchScheduler,
                _.options);
        }

        MemberExpression member = (MemberExpression)selector.Body;
        IDataLoader loader = _loaders.GetOrAdd(
            member.Member, 
            (_, args) => CreateLoader(args),
            (selector, _reader, _batchScheduler, _options));
        return (ByFilterLoader<TSource, TValue>)loader;
    }

    public void Dispose()
    {
        if (_isDisposed) return;

        _isDisposed = true;
        foreach (IDisposable loader in _loaders.Values.Cast<IDisposable>())
            loader.Dispose();
    }
}
