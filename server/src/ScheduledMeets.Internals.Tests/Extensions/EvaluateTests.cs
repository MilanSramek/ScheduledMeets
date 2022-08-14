using FluentAssertions;

using Moq;

using ScheduledMeets.Internals.Extensions;
using ScheduledMeets.TestTools.Extensions;

using System.Collections;

namespace ScheduledMeets.Internals.Tests.Extensions;

public class EvaluateTests
{
    class EmptyCollectionTestCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return Enumerable.Empty<int>();
            yield return Array.Empty<int>();
        }
    }

    [Test]
    [TestCaseSource(typeof(EmptyCollectionTestCases))]
    public void EmptyCollectionTest(IEnumerable<int> collection)
    {
        IReadOnlyList<int>? result = collection.Evaluate();
        result.Should().BeEmpty();
    }

    class ReadOnlyListTestCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new List<int> { 1, 2, 5 };
            yield return new int[] { 5, 4, 2 };
            yield return Mock.Of<IReadOnlyList<int>>();
        }
    }

    [Test]
    [TestCaseSource(typeof(ReadOnlyListTestCases))]
    public void ReadOnlyListsTest(IReadOnlyList<int> collection)
    {
        IReadOnlyList<int>? result = collection.Evaluate();
        result.Should().BeSameAs(collection);
    }

    class EnumerableTestCases : IEnumerable
    {
        private IEnumerable<int> Enumerable()
        {
            yield return 9;
            yield return 18;
            yield return 91;
            yield return -1890;
        }

        public IEnumerator GetEnumerator()
        {
            yield return Enumerable();
        }
    }

    [Test]
    [TestCaseSource(typeof(EnumerableTestCases))]
    public void EnumerablesTest(IEnumerable<int> collection)
    {
        IReadOnlyList<int>? result = collection.Evaluate();
        result.Should().ContainExactly(collection);
    }
}
