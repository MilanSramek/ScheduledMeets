using ScheduledMeets.Internals.Collections;

namespace ScheduledMeets.Core.MeetsScheduling;

public class DayOfWeekOccurence : ISequence<DateTime, DateTime>
{
    public DayOfWeekOccurence(DayOfWeek day) => Day = day;

    public DayOfWeek Day { get; set; }

    public IEnumerator<DateTime> GetEnumeratorFrom(DateTime cursor)
    {
        DateOnly date = DateOnly.FromDateTime(cursor);
        DayOfWeek cursoredDay = date.DayOfWeek;

        int dayDifference = Day - cursoredDay;
        if (dayDifference < 0)
            dayDifference = 7 + dayDifference;

        DateTime item = date.AddDays(dayDifference).ToDateTime(TimeOnly.FromDateTime(cursor));

        while (true)
        {
            yield return item;
            item += TimeSpan.FromDays(7);
        }
    }
}
