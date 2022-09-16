using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightsByArrivalAirport
{
    [TestClass]
    public class GetFlightsByArrivalAirportQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnICollectionOfFlightDTO()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightsByArrivalAirportQuery("Kiev");

            var flights = new List<Flight>();
            var flightDTOs = new List<FlightDTO>();

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByFlightArrivalAirport(query, cancellationToken))
                .ReturnsAsync(flights);
            mockCaster.Setup(f => f.Cast(flights, cancellationToken)).ReturnsAsync(flightDTOs);

            var handler = new GetFLightsByArrivalAirportQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = flightDTOs;

            // act
            var actualResult = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.SearchByFlightArrivalAirport(query, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(flights, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryArrivalAirportNameIsTooBig_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightsByArrivalAirportQuery(new string('a', 51));
            var handler = new GetFLightsByArrivalAirportQueryHandler(mockRepository.Object, mockCaster.Object);

            var cancellationToken = new CancellationTokenSource().Token;

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(query, cancellationToken)).Wait();
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryArrivalAirportNameIsEmpty_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightsByArrivalAirportQuery(string.Empty);
            var handler = new GetFLightsByArrivalAirportQueryHandler(mockRepository.Object, mockCaster.Object);

            var cancellationToken = new CancellationTokenSource().Token;

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(query, cancellationToken)).Wait();
        }
    }
}
