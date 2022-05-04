namespace ScheduledMeets.Internals.Extensions;

public static class TaskExtensions
{
    public static async Task<(TResult1 Result1, TResult2 Result2)> And<TResult1, TResult2>(
        this Task<TResult1> task1,
        Task<TResult2> task2)
    {
        await Task.WhenAll(task1, task2);
        return (task1.Result, task2.Result);
    }
}
