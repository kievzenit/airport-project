using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Flights.Commands.DeleteFlight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Flights.Commands.DeleteFlight
{
    [TestClass]
    public class DeleteFlightCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValidAndFlightExists_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);

            var command = new DeleteFlightCommand(9);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Delete(command, cancellationToken)).ReturnsAsync(true);

            var handler = new DeleteFlightCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.Delete(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValidAndDoesNotFlightExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);

            var command = new DeleteFlightCommand(9);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Delete(command, cancellationToken)).ReturnsAsync(false);

            var handler = new DeleteFlightCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(() => handler.Handle(command, cancellationToken));
            mockRepository.Verify(f => f.Delete(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);

            var command = new DeleteFlightCommand(-9);

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new DeleteFlightCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken));
        }
    }
}
