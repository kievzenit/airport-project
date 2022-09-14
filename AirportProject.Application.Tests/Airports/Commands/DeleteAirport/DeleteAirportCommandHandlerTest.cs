using AirportProject.Application.Airports.Commands.DeleteAirport;
using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Airports.Commands.DeleteAirport
{
    [TestClass]
    public class DeleteAirportCommandHandlerTest
    {
        [TestMethod]
        public void Test_HandleMethod_When_DataIsValid_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new DeleteAirportCommand(5);

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.Delete(command.Id, cancellationToken)).ReturnsAsync(true);

            var handler = new DeleteAirportCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.Delete(command.Id, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_HandleMethod_When_DataIsInValid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new DeleteAirportCommand(-5);

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var handler = new DeleteAirportCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken));
        }

        [TestMethod]
        public void Test_HandleMethod_When_DataIsValidAndAirportDoesNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new DeleteAirportCommand(5);

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.Delete(command.Id, cancellationToken)).ReturnsAsync(false);

            var handler = new DeleteAirportCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(() => handler.Handle(command, cancellationToken));
            mockRepository.Verify(f => f.Delete(command.Id, cancellationToken), Times.Once);
        }
    }
}
