using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Commands.RemoveTicketFromPassenger
{
    [TestClass]
    public class RemoveTicketFromPassengerCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerDoNotHaveThisTicket_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = 23;
            const int ticketId = 862;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.DeleteTicket(command, cancellationToken)).ReturnsAsync(true);

            var handler = new RemoveTicketFromPassengerCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken);

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.DeleteTicket(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerOrTicketDoNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = 23;
            const int ticketId = 862;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.DeleteTicket(command, cancellationToken)).ReturnsAsync(false);

            var handler = new RemoveTicketFromPassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(
                () => handler.Handle(command, cancellationToken)).Wait();
            mockRepository.Verify(f => f.DeleteTicket(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            const int passengerId = -23;
            const int ticketId = -862;
            var command = new RemoveTicketFromPassengerCommand(passengerId, ticketId);

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new RemoveTicketFromPassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(command, cancellationToken)).Wait();
        }
    }
}
