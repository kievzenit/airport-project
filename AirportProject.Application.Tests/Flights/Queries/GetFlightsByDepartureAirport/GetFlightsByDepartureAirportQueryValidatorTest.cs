using AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightsByDepartureAirport
{
    [TestClass]
    public class GetFlightsByDepartureAirportQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldreturnTrue()
        {
            // arrange
            var query = new GetFlightsByDepartureAirportQuery("Kiev");

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryAirportNameIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightsByDepartureAirportQuery(new string('a', 51));

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryAirportNameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightsByDepartureAirportQuery(string.Empty);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
