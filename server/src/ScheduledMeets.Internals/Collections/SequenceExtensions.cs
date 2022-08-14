using System.Diagnostics.CodeAnalysis;

namespace ScheduledMeets.Internals.Collections
{
    public static class SequenceExtensions
    {
        public static IEnumerable<TElement> TakeAfter<TCursor, TElement>(
            this ISequence<TCursor, TElement> sequence,
            int count,
            TCursor cursor)
        {
            ArgumentNullException.ThrowIfNull(sequence);
            ArgumentNullException.ThrowIfNull(cursor);
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            IEnumerator<TElement> enumerator = sequence.GetEnumeratorFrom(cursor);

            for (int i = 0; i < count; i++)
            {
                if (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                    continue;
                }
                
                break;
            }
        }

        public static bool TryGetFirstAfter<TCursor, TElement>(
            this ISequence<TCursor, TElement> sequence,
            TCursor cursor,
            [MaybeNullWhen(false)]out TElement firstAfter)
        {
            ArgumentNullException.ThrowIfNull(sequence);
            ArgumentNullException.ThrowIfNull(cursor);

            IEnumerator<TElement> enumerator = sequence.GetEnumeratorFrom(cursor);
            if (enumerator.MoveNext())
            {
                firstAfter = enumerator.Current;
                return true;
            }

            firstAfter = default;
            return false;
        }

        public static TElement FirstAfter<TCursor, TElement>(
            this ISequence<TCursor, TElement> sequence,
            TCursor cursor)
        {
            if (sequence.TryGetFirstAfter(cursor, out TElement? firstAfter))
                return firstAfter;

            throw new InvalidOperationException("The source sequence is empty");
        }
    }
}
