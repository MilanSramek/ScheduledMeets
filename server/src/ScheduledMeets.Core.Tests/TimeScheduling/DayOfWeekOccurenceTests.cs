using FluentAssertions;
using FluentAssertions.Extensions;

using ScheduledMeets.Core.MeetsScheduling;
using ScheduledMeets.Internals.Collections;
using ScheduledMeets.TestTools.Extensions;

namespace ScheduledMeets.Core.Tests.TimeScheduling;

public class DayOfWeekOccurenceTests
{
    [Test]
    public void ValuesAfterTest([Values] DayOfWeek dayOfWeek)
    {
        DayOfWeekOccurence sut = new(dayOfWeek);

        DateTime dayCursor = new(2000, 1, 7);
        TimeSpan timeCursor = new(123456789098);

        IEnumerable<DateTime> values = sut
            .TakeAfter(10, dayCursor + timeCursor)
            .ToList();

        values.Should().AllSatisfy(value =>
        {
            value.DayOfWeek.Should().Be(dayOfWeek);
            value.TimeOfDay.Should().Be(timeCursor);
        });

        values.Select((DateTime previous, DateTime current) => current - previous)
            .Should().AllSatisfy(diff => diff.Should().Be(7.Days()));
    }
}
