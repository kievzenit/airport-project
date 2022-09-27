using AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Commands.RemoveTicketFromPassenger
{
    [TestClass]
    public class RemoveTicketFromPassengerCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_CommandIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            const int passengerId = 386;
            const int ticketId = 876;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_CommandIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = -386;
            const int ticketId = -876;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_PassengerIdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = -386;
            const int ticketId = 876;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_PassengerIdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = 0;
            const int ticketId = 876;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_TicketIdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = 386;
            const int ticketId = -876;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_TicketIdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = 386;
            const int ticketId = 0;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
