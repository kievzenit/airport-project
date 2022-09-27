using AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Tickets.Queries.GetTicketsByPassengerId
{
    [TestClass]
    public class GetTicketsByPassengerIdQueryValidator
    {
        [TestMethod]
        public void Test_QueryValidator_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetTicketsByPassengerIdQuery(38);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PassengerIdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetTicketsByPassengerIdQuery(-38);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PAssengerIdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetTicketsByPassengerIdQuery(0);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
