using System.Linq;
using FluentAssertions;
using Xunit;

namespace TspSA.Domain.Tests
{
    public class TourFacts
    {
        [Fact]
        public void TotalDistanceReturnsSumOfRouteDistances()
        {
            var tour = new Tour(
                new City(0, 0),
                new City(3, 4),
                new City(-3, 4)
                );

            tour.TotalDistance.Should().Be(16d);
        }

        [Fact]
        public void SwapingCitiesModifiesSequenceOfCitiesInTheRoute()
        {
            City s1, s2, s3;
            var tour = new Tour(
                s1 = new City(0, 0),
                s2 = new City(3, 4),
                s3 = new City(-3, 4)
                );

            var tour2 = tour.Swap(0, 2);
            tour2.Route.First().Should().Be(s3);
            tour2.Route.Last().Should().Be(s1);
        }
    }
}
