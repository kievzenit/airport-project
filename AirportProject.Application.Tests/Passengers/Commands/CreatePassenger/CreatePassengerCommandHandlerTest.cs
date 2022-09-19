using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Passengers.Commands.CreatePassenger;
using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Commands.CreatePassenger
{
    [TestClass]
    public class CreatePassengerCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerDoesNotExists_Then_ShoulReturnPassengerDTO()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jason",
                Passport = "ad367822",
                Nationality = "American",
                Gender = "male",
                Birthday = DateTime.Parse("22/05/19")
            };
            var passenger = new Passenger
            {
                Id = 9,
                Firstname = command.Firstname,
                Lastname = command.Lastname,
                Passport = command.Passport,
                Nationality = command.Nationality,
                Gender = command.Gender,
                Birthday = command.Birthday
            };
            var passengerDTO = new PassengerDTO
            {
                Id = passenger.Id,
                Firstname = passenger.Firstname,
                Lastname = passenger.Lastname,
                Passport = passenger.Passport,
                Nationality = passenger.Nationality,
                Gender = passenger.Gender,
                Birthday = passenger.Birthday
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Create(command, cancellationToken)).ReturnsAsync(passenger);
            mockRepository.Setup(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(default(Passenger));

            mockCaster.Setup(f => f.Cast(passenger, cancellationToken)).ReturnsAsync(passengerDTO);

            var handler = new CreatePassengerCommandHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = passengerDTO;

            // act
            var actualResult = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.Create(command, cancellationToken), Times.Once);
            mockRepository.Verify(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
            mockCaster.Verify(f => f.Cast(passenger, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsValidAndPassengerExists_Then_ShoulThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jason",
                Passport = "ad367822",
                Nationality = "American",
                Gender = "male",
                Birthday = DateTime.Parse("22/05/19")
            };
            var existingPassenger = new Passenger
            {
                Id = 9,
                Firstname = "Elvira",
                Lastname = "Jason",
                Passport = "ad367822",
                Nationality = "American",
                Gender = "male",
                Birthday = DateTime.Parse("22/05/19")
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(existingPassenger);

            var handler = new CreatePassengerCommandHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken))
                .Wait();

            mockRepository.Verify(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_CommandIsInValid_Then_ShoulThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = string.Empty,
                Lastname = new string('a', 52),
                Passport = "a",
                Nationality = string.Empty,
                Gender = "notexisting",
                Birthday = DateTime.MaxValue
            };

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new CreatePassengerCommandHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken)).Wait();
        }
    }
}
