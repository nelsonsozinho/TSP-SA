using FluentAssertions;
using Xunit;

namespace TspSA.Domain.Tests
{
    public class CityFacts
    {
        [Fact]
        public void DistanceOfACityIn_0_0_ToAnotherIn_3_4_Is_5()
        {
            var city1 = new City(0, 0);
            var city2 = new City(3, 4);
            city1.DistanceTo(city2).Should().Be(5);
        }
    }
}
