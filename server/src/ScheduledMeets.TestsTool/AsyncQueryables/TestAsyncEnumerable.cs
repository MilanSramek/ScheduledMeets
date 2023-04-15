namespace ScheduledMeets.TestTools.AsyncQueryables;

internal class TestAsyncEnumerable<TElement> : IAsyncEnumerable<TElement>
{
    public class Enumerator : IAsyncEnumerator<TElement>
    {
        private readonly IEnumerator<TElement> _enumerator;

        public Enumerator(IEnumerator<TElement> enumerator)
        {
            _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
        }

        public TElement Current => _enumerator.Current;

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        public ValueTask<bool> MoveNextAsync() => new(_enumerator.MoveNext());
    }

    private readonly IEnumerable<TElement> _enumerable;

    public TestAsyncEnumerable(IEnumerable<TElement> enumerable)
    {
        _enumerable = enumerable ?? throw new ArgumentNullException(nameof(enumerable));
    }

    public IAsyncEnumerator<TElement> GetAsyncEnumerator(
        CancellationToken cancellationToken = default)
    {
        return new Enumerator(_enumerable.GetEnumerator());
    }
}
