using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Flights.Queries.GetFlightsWithPagination;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightsWithPagination
{
    [TestClass]
    public class GetFlightsWitPaginationQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnICollectionOfFlightDTOs()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            const int totalFlightsCount = 34;

            var query = new GetFlightsWithPaginationQuery(1);
            var flights = new List<Flight>();
            var flightDTOs = new List<FlightDTO>();

            var cancellationtoken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.GetRange(query.PageNumber, query.PageSize, cancellationtoken))
                .ReturnsAsync(flights);
            mockRepository.Setup(f => f.GetTotalCount(cancellationtoken)).ReturnsAsync(totalFlightsCount);
            mockCaster.Setup(f => f.Cast(flights, cancellationtoken)).ReturnsAsync(flightDTOs);

            var handler = new GetFlightsWithPaginationQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedItemsResult = flightDTOs;
            var expectedtTotalCountResult = totalFlightsCount;

            // act
            var result = handler.Handle(query, cancellationtoken).Result;

            // assert
            Assert.AreEqual(expectedItemsResult, result.Items);
            Assert.AreEqual(expectedtTotalCountResult, result.TotalCount);
            mockRepository.Verify(f =>
                f.GetRange(query.PageNumber, query.PageSize, cancellationtoken), Times.Once);
            mockRepository.Verify(f => f.GetTotalCount(cancellationtoken), Times.Once);
            mockCaster.Verify(f => f.Cast(flights, cancellationtoken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryPageNumberIsNegative_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightsWithPaginationQuery(-1);
            var cancellationtoken = new CancellationTokenSource().Token;
            var handler = new GetFlightsWithPaginationQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(query, cancellationtoken)).Wait();
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryPageNumberIsZero_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightsWithPaginationQuery(0);
            var cancellationtoken = new CancellationTokenSource().Token;
            var handler = new GetFlightsWithPaginationQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(query, cancellationtoken)).Wait();
        }
    }
}
