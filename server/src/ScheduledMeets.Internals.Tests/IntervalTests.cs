using FluentAssertions;

using NUnit.Framework;

namespace ScheduledMeets.Internals.Tests
{
    public class IntervalTests
    {
        [Test]
        public void EqualsTest()
        {
            Interval<double> sut = new(0d, 1d);
            sut.Should().BeEquivalentTo(new Interval<double>(0d, 1d));
        }

        [Test]
        public void NonEmptyIntersectionTest()
        {
            Interval<double> sut1 = new(0d, 10d);
            Interval<double> sut2 = new(5d, 20d);

            Interval<double>? intersection = sut1.Intersection(sut2);

            intersection.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new Interval<double>(5d, 10d));
        }

        [Test]
        public void EmptyIntersectionOfAdjacentIntervalsTest()
        {
            Interval<double> sut1 = new(0d, 3d);
            Interval<double> sut2 = new(3d, 20d);

            Interval<double>? intersection = sut1.Intersection(sut2);

            intersection.Should().BeNull();
        }

        [Test]
        public void EmptyIntersectionTest()
        {
            Interval<double> sut1 = new(0d, 3d);
            Interval<double> sut2 = new(4d, 20d);

            Interval<double>? intersection = sut1.Intersection(sut2);

            intersection.Should().BeNull();
        }

        static readonly Interval<int>[] _containsTestSource = new Interval<int>[]
        {
            new(10, 15),
            new(20, 22),
            new(21, 30),
            new(10, 30)
        };

        [Test, TestCaseSource(nameof(_containsTestSource))]
        public void ContainsTest(Interval<int> other)
        {
            Interval<int> sut = new(10, 30);

            sut.Contains(other).Should().BeTrue();
        }
    }
}
