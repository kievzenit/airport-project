using AirportProject.Application.Flights.Queries.GetFlightsWithPagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightsWithPagination
{
    [TestClass]
    public class GetFlightsWithPaginationQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetFlightsWithPaginationQuery(5);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_QueryPageNumberIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightsWithPaginationQuery(-95);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_QueryPageNumberIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetFlightsWithPaginationQuery(0);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
