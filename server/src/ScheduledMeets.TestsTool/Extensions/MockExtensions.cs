using Moq;

namespace ScheduledMeets.TestTools.Extensions
{
    public static class MockExtensions
    {
        public static void SetupAsAsyncQueryable<TQueryable, TElement>(this Mock<TQueryable> mock,
            IEnumerable<TElement> data) 
            where TQueryable : class, IQueryable<TElement>
        {
            IQueryable<TElement> queryable = data.AsAsyncQueryable();

            mock
                .SetupGet(_ => _.Expression)
                .Returns(queryable.Expression);
            mock
                .SetupGet(_ => _.Provider)
                .Returns(queryable.Provider);
            mock
                .SetupGet(_ => _.ElementType)
                .Returns(queryable.ElementType);
            mock
                .Setup(_ => _.GetEnumerator())
                .Returns(queryable.GetEnumerator());
            mock.As<IQueryable>()
                .Setup(_ => _.GetEnumerator())
                .Returns(queryable.GetEnumerator());
        }
    }
}
