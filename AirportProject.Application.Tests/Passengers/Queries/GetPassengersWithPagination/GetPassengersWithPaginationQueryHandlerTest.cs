using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Passengers.Queries.GetPassengersWithPagination;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengersWithPagination
{
    [TestClass]
    public class GetPassengersWithPaginationQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnICollectionOfPassengerDTO()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var query = new GetPassengersWithPaginationQuery(5);
            const int totalPassengersCount = 87;
            var passengers = new List<Passenger>();
            var passengerDTOs = new List<PassengerDTO>();

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.GetRange(query, cancellationToken)).ReturnsAsync(passengers);
            mockRepository.Setup(f => f.GetTotalCount(cancellationToken)).ReturnsAsync(totalPassengersCount);
            mockCaster.Setup(f => f.Cast(passengers, cancellationToken)).ReturnsAsync(passengerDTOs);

            var handler = new GetPassengersWithPaginatonQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedItemsResult = passengerDTOs;
            var expectedTotalCount = totalPassengersCount;

            // act
            var result = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedItemsResult, result.Items);
            Assert.AreEqual(expectedTotalCount, result.TotalCount);
            mockRepository.Verify(f => f.GetRange(query, cancellationToken), Times.Once);
            mockRepository.Verify(f => f.GetTotalCount(cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(passengers, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_PageNumberIsZero_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var query = new GetPassengersWithPaginationQuery(0);
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetPassengersWithPaginatonQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }

        [TestMethod]
        public void Test_QueryHandler_When_PageNumberIsNegative_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Passenger, PassengerDTO>>(MockBehavior.Strict);

            var query = new GetPassengersWithPaginationQuery(-6);
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetPassengersWithPaginatonQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }
    }
}
