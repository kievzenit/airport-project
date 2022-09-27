using AirportProject.Application.Tickets.Queries.GetSpecificTickets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Tickets.Queries.GetSpecificTickets
{
    [TestClass]
    public class GetSpecificTicketsQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = "Kiev",
                To = "Berlin",
                Type = "economy"
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_FromValueIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = string.Empty,
                To = "Berlin",
                Type = "economy"
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_FromValueIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = new string('a', 51),
                To = "Berlin",
                Type = "economy"
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_ToValueIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = "Keiv",
                To = string.Empty,
                Type = "economy"
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_ToValueIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = "Keiv",
                To = new string('a', 51),
                Type = "economy"
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_TypeIsEmpty_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = "Kiev",
                To = "Berlin",
                Type = string.Empty
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_TypeIsInvalid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetSpecificTicketsQuery
            {
                From = "Kiev",
                To = "Berlin",
                Type = "doesnotexisting"
            };

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
