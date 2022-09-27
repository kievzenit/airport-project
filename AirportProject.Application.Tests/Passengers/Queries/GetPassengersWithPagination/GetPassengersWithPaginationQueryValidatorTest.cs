using AirportProject.Application.Passengers.Queries.GetPassengersWithPagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengersWithPagination
{
    [TestClass]
    public class GetPassengersWithPaginationQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetPassengersWithPaginationQuery(5);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PageNumberIsZero_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetPassengersWithPaginationQuery(0);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PageNumberIsNegative_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetPassengersWithPaginationQuery(-5);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
