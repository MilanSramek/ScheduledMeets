namespace ScheduledMeets.Connectivity.Tools;

public class TimeProvider : ITimeProvider
{
    public DateTime GetCurrentTime() => DateTime.Now;
}
