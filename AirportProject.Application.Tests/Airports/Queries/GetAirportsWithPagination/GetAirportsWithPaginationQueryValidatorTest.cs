using AirportProject.Application.Airports.Queries.GetAirportsWithPagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Airports.Queries.GetAirportsWithPagination
{
    [TestClass]
    public class GetAirportsWithPaginationQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_InputDataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetAirportsWithPaginationQuery(5);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_InputDataIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetAirportsWithPaginationQuery(-5);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
