using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Flights.Commands.UpdateFlight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Flights.Commands.UpdateFlight
{
    [TestClass]
    public class UpdateFlightCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValidAndFlightIsExsits_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);

            var command = new UpdateFlightCommand
            {
                Id = 860,
                ArrivalAirportName = "Kiev",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:15"),
                DepartureTime = DateTime.Parse("2/09/15 09:15"),
                Status = "delayed",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Update(command, cancellationToken)).ReturnsAsync(true);

            var handler = new UpdateFlightCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.Update(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValidAndFlightDoesNotExsit_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);

            var command = new UpdateFlightCommand
            {
                Id = 860,
                ArrivalAirportName = "Kiev",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:15"),
                DepartureTime = DateTime.Parse("2/09/15 09:15"),
                Status = "delayed",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Update(command, cancellationToken)).ReturnsAsync(false);

            var handler = new UpdateFlightCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(() => handler.Handle(command, cancellationToken)).Wait();
            mockRepository.Verify(f => f.Update(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsInalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);

            var command = new UpdateFlightCommand
            {
                Id = -860,
                ArrivalAirportName = "Kiev",
                Terminal = '5',
                ArrivalTime = DateTime.Parse("22/09/15 15:15"),
                DepartureTime = DateTime.Parse("2/09/15 09:15"),
                Status = "notexisting",
                EconomyPrice = 10,
                BusinessPrice = 1200
            };

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new UpdateFlightCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken)).Wait();
        }
    }
}
