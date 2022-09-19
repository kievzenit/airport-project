using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Passengers.Commands.UpdatePassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Commands.UpdatePassenger
{
    [TestClass]
    public class UpdatePassengerCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerExists_Then_ShoulReturnTrue()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new UpdatePassengerCommand
            {
                Id = 99,
                Firstname = "Elvira",
                Lastname = "Jason",
                Passport = "ad367822",
                Nationality = "American"
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Update(command, cancellationToken)).ReturnsAsync(true);

            var handler = new UpdatePassengerCommandHandler(mockRepository.Object);

            // act
            var result = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.IsNotNull(result);
            mockRepository.Verify(f => f.Update(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerDoesNotExist_Then_ShoulThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = "Jason",
                Passport = "ad367822",
                Nationality = "American"
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Update(command, cancellationToken)).ReturnsAsync(false);

            var handler = new UpdatePassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(
                () => handler.Handle(command, cancellationToken)).Wait();
            mockRepository.Verify(f => f.Update(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsInValid_Then_ShoulThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new UpdatePassengerCommand
            {
                Id = -76,
                Firstname = string.Empty,
                Lastname = new string('a', 52),
                Passport = "a",
                Nationality = string.Empty
            };

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new UpdatePassengerCommandHandler(mockRepository.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(command, cancellationToken)).Wait();
        }
    }
}
