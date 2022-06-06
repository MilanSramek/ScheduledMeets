using FluentAssertions;

using NUnit.Framework;

using ScheduledMeets.Internals.Collections;
using ScheduledMeets.TestTools.Extensions;

namespace ScheduledMeets.Internals.Tests.Collections;

public class IntervalsTests
{
    [Test]
    public void SubtrackInnerIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(1, 3));

        sut.Should().ContainExactly(new[]
        {
            new Interval<int>(0, 1),
            new Interval<int>(3, 10)
        });
    }

    [Test]
    public void SubtrackLeftIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(-1, 2));

        sut.Should().ContainSingle(new Interval<int>(2, 10));
    }

    [Test]
    public void SubtrackLeftInnerIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(0, 2));

        sut.Should().ContainSingle(new Interval<int>(2, 10));
    }

    [Test]
    public void SubtrackRightIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(7, 20));

        sut.Should().ContainSingle(new Interval<int>(0, 7));
    }

    [Test]
    public void SubtrackRightInnerIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(8, 10));

        sut.Should().ContainSingle(new Interval<int>(0, 8));
    }

    [Test]
    public void SubtrackOuterIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(11, 20));

        sut.Should().ContainSingle(new Interval<int>(0, 10));
    }

    [Test]
    public void SubtractGreaterIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Subtract(new(-20, 20));

        sut.Should().BeEmpty();
    }

    [Test]
    public void SubtractInterIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10), new(12, 15), new(20, 30) });
        sut.Subtract(new(8, 22));

        sut.Should().ContainExactly(new[]
       {
            new Interval<int>(0, 8),
            new Interval<int>(22, 30)
        });
    }

    [Test]
    public void UnionInnerIntervalTest()
    {
        Intervals<int> sut = new(new Interval<int>[] { new(0, 10) });
        sut.Union(new(1, 9));

        sut.Should().ContainSingle(new Interval<int>(0, 10));
    }

    static readonly Interval<int>[] _unionRightInnerIntervalTestSource = new Interval<int>[]
    {
        new(-8, 200),
        new(0, 200),
        new(100, 200)
    };

    [Test, TestCaseSource(nameof(_unionRightInnerIntervalTestSource))]
    public void UnionRightInnerIntervalTest(Interval<int> other)
    {
        Intervals<int> sut = new(new Interval<int>[] { new(-8, 100) });
        sut.Union(other);

        sut.Should().ContainSingle(new Interval<int>(-8, 200));
    }

    static readonly Interval<int>[] _unionLeftInnerIntervalTestSource = new Interval<int>[]
    {
        new(-100, -10),
        new(-100, -20),
        new(-100, -80)
    };

    [Test, TestCaseSource(nameof(_unionLeftInnerIntervalTestSource))]
    public void UnionLeftInnerIntervalTest(Interval<int> other)
    {
        Intervals<int> sut = new(new Interval<int>[] { new(-80, -10) });
        sut.Union(other);

        sut.Should().ContainSingle(new Interval<int>(-100, -10));
    }

    static readonly Interval<int>[] _unionOverlappingIntervalTestSource = new Interval<int>[]
    {
        new(-80, 10),
        new(-80, 1),
        new(-80, 5),
        new(-20, 7),
        new(-10, 1),
        new(-10, 5),
        new(-10, 10),
    };

    [Test, TestCaseSource(nameof(_unionOverlappingIntervalTestSource))]
    public void UnionOverlappingIntervalTest(Interval<int> other)
    {
        Intervals<int> sut = new(new Interval<int>[] { new(-80, -10), new(1, 10) });
        sut.Union(other);

        sut.Should().ContainSingle(new Interval<int>(-80, 10));
    }
}
