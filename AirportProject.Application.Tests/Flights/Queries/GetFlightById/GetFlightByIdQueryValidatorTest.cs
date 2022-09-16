using AirportProject.Application.Flights.Queries.GetFlightById;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightById
{
    [TestClass]
    public class GetFlightByIdQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_DataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetFlightByIdQuery(8);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_DataIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightByIdQuery(-8);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_IdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightByIdQuery(0);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
