using FluentAssertions;
using FluentAssertions.Collections;

namespace ScheduledMeets.TestTools.Extensions
{
    public static class GenericCollectionAssertionsExtensions
    {
        public static AndConstraint<GenericCollectionAssertions<T>> ContainExactly<T>(
            this GenericCollectionAssertions<T> assertions!!,
            IEnumerable<T> expectedSuperset!!, 
            string because = "",
            params object[] becauseArgs)
        {
            return assertions
                .Contain(expectedSuperset, because, becauseArgs)
                .And.BeSubsetOf(expectedSuperset, because, becauseArgs);
        }

        public static AndConstraint<GenericCollectionAssertions<T>> ContainSingle<T>(
            this GenericCollectionAssertions<T> assertions!!,
            T expectedItem,
            string because = "",
            params object[] becauseArgs)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            return assertions
                .ContainSingle(item => comparer.Equals(item, expectedItem), because, becauseArgs);
        }
    }
}
