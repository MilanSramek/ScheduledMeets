namespace ScheduledMeets.TestTools;

public static class PostponedException
{
    public static async Task<object> Schedule(TimeSpan delay)
    {
        await Task.Delay(delay);
        throw new TimeoutException("Timeout exceeded.");
    }
}
