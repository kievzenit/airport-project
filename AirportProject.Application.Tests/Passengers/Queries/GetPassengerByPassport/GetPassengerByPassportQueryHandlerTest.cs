using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengerByPassport
{
    [TestClass]
    public class GetPassengerByPassportQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValidAndPassengerExists_Then_ShouldReturnPassengerDTO()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            const string passport = "ad228765";
            var query = new GetPassengerByPassportQuery(passport);
            var passenger = new Passenger
            {
                Id = 98,
                Firstname = "Leo",
                Lastname = "Lewis",
                Passport = passport,
                Nationality = "British",
                Birthday = new DateTime(1990, 6, 4),
                Gender = "male"
            };
            var passengerDTO = new PassengerDTO
            {
                Id = passenger.Id,
                Firstname = passenger.Firstname,
                Lastname = passenger.Lastname,
                Passport = passenger.Passport,
                Nationality = passenger.Nationality,
                Birthday = passenger.Birthday,
                Gender = passenger.Gender
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByPassport(query, cancellationToken)).ReturnsAsync(passenger);
            mockCaster.Setup(f => f.Cast(passenger, cancellationToken)).ReturnsAsync(passengerDTO);

            var handler = new GetPassengerByPassportQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = passengerDTO;

            // act
            var actualResult = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.SearchByPassport(query, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(passenger, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            const string passport = "ad22876565757";
            var query = new GetPassengerByPassportQuery(passport);
            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new GetPassengerByPassportQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValidAndPassengerDoesNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            const string passport = "ad228765";
            var query = new GetPassengerByPassportQuery(passport);
            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByPassport(query, cancellationToken))
                .ReturnsAsync(default(Passenger));

            var handler = new GetPassengerByPassportQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();

            mockRepository.Verify(f => f.SearchByPassport(query, cancellationToken), Times.Once);
        }
    }
}
