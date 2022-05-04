namespace ScheduledMeets.TestTools.Extensions;

public static class TaskExtensions
{
    public static async Task SetTimeout<TResult>(this Task task, TimeSpan timeout)
    {
        await await Task.WhenAny(task, PostponedException.Schedule(timeout));
    }

    public static async Task<TResult> SetTimeout<TResult>(this Task<TResult> task,
        TimeSpan timeout)
    {
        await await Task.WhenAny(task, PostponedException.Schedule(timeout));
        return task.Result;
    }
}
