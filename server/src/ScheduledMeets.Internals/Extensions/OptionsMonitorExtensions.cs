using Microsoft.Extensions.Options;

namespace ScheduledMeets.Internals.Extensions
{
    public static class OptionsMonitorExtensions
    {
        public static IDisposable OnChange<TOptions>(
            this IOptionsMonitor<TOptions> monitor,
            string name, 
            Action<TOptions> listener)
        {
            return monitor.OnChange((options, n) =>
            {
                if (name == n)
                    listener(options);
            });
        }
    }
}
