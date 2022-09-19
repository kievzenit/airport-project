using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Passengers.Commands.DeletePassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Commands.DeletePassenger
{
    [TestClass]
    public class DeletePassengerCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerExists_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new DeletePassengerCommand(7);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Delete(command, cancellationToken)).ReturnsAsync(true);

            var handler = new DeletePassengerCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.Delete(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerDoesNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new DeletePassengerCommand(7);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Delete(command, cancellationToken)).ReturnsAsync(false);

            var handler = new DeletePassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(() => handler.Handle(command, cancellationToken))
                .Wait();
            mockRepository.Verify(f => f.Delete(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new DeletePassengerCommand(-87);
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new DeletePassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken))
                .Wait();
        }
    }
}
