using AirportProject.Application.Passengers.Commands.AddTicketToPassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Commands.AddTicketToPassenger
{
    [TestClass]
    public class AddTicketToPassengerCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_CommandIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            const int passengerId = 34;
            const int ticketId = 387;

            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_CommandIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = -34;
            const int ticketId = -387;

            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_PassengerIdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = -34;
            const int ticketId = 387;

            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

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
            const int ticketId = 387;

            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_TicketIdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = 34;
            const int ticketId = -387;

            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_TicketIdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            const int passengerId = 34;
            const int ticketId = 0;

            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
