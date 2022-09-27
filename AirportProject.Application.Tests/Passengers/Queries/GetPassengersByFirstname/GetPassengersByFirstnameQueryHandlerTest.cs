using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Passengers.Queries.GetPassengersByFirstname;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengersByFirstname
{
    [TestClass]
    public class GetPassengersByFirstnameQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnICollectionOfPassengerDTO()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var query = new GetPassengersByFirstnameQuery("Leo");
            var passengers = new List<Passenger>();
            var passengerDTOs = new List<PassengerDTO>();

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByFirstname(query, cancellationToken)).ReturnsAsync(passengers);
            mockCaster.Setup(f => f.Cast(passengers, cancellationToken)).ReturnsAsync(passengerDTOs);

            var handler = new GetPassengersByFirstnameQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = passengerDTOs;

            // act
            var actualResult = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.SearchByFirstname(query, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(passengers, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_FirstnameIsEmpty_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var query = new GetPassengersByFirstnameQuery(string.Empty);
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetPassengersByFirstnameQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }

        [TestMethod]
        public void Test_QueryHandler_When_FirstnameIstooLong_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var query = new GetPassengersByFirstnameQuery(new string('a', 51));
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetPassengersByFirstnameQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }
    }
}
