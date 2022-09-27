using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Passengers.Commands.AddTicketToPassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Commands.AddTicketToPassenger
{
    [TestClass]
    public class AddTicketToPassengerCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerDoNotHaveThisTicket_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = 23;
            const int ticketId = 862;
            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.AddTicket(command, cancellationToken)).ReturnsAsync(true);

            var handler = new AddTicketToPassengerCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken);

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.AddTicket(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerOrTicketDoNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = 23;
            const int ticketId = 862;
            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.AddTicket(command, cancellationToken)).ReturnsAsync(false);

            var handler = new AddTicketToPassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(
                () => handler.Handle(command, cancellationToken)).Wait();
            mockRepository.Verify(f => f.AddTicket(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerHaveThisTicket_Then_ShouldThrowInvalidOperationException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = 23;
            const int ticketId = 862;
            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.AddTicket(command, cancellationToken))
                .ThrowsAsync(new InvalidOperationException());

            var handler = new AddTicketToPassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => handler.Handle(command, cancellationToken)).Wait();
            mockRepository.Verify(f => f.AddTicket(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = -23;
            const int ticketId = -862;
            var command = new AddTicketToPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new AddTicketToPassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(command, cancellationToken)).Wait();
        }
    }
}
