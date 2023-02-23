namespace ScheduledMeets.View;

public interface IReader<TView> : IQueryable<TView>, IAsyncEnumerable<TView>
{
}