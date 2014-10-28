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
    }
}
