using AirportProject.Application.Airports.Commands.UpdateAirport;
using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Airports.Commands.UpdateAirport
{
    [TestClass]
    public class UpdateAirportCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValidAndAirportExists_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new UpdateAirportCommand
            {
                Id = 24,
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.Update(command, cancellationToken)).ReturnsAsync(true);

            var handler = new UpdateAirportCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.Update(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsInValid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new UpdateAirportCommand
            {
                Id = -24,
                Name = new string('a', 51),
                City = "Borispol",
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var handler = new UpdateAirportCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken)).Wait();
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValidAndAirportDoesNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new UpdateAirportCommand
            {
                Id = 24,
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.Update(command, cancellationToken)).ReturnsAsync(false);

            var handler = new UpdateAirportCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(() => handler.Handle(command, cancellationToken)).Wait();
            mockRepository.Verify(f => f.Update(command, cancellationToken), Times.Once);
        }
    }
}
