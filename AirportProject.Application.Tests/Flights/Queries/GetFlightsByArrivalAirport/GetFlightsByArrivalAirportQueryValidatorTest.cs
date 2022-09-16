using AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightsByArrivalAirport
{
    [TestClass]
    public class GetFlightsByArrivalAirportQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetFlightsByArrivalAirportQuery("Kiev");

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryAirportNameIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightsByArrivalAirportQuery(new string('a', 51));

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryAirportNameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightsByArrivalAirportQuery(string.Empty);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
