using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Flights.Queries.GetFlightById;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Flights.Queries.GetFlightById
{
    [TestClass]
    public class GetFlightByIdQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValidAndFlightIsExists_Then_ShouldReturnFLightDTO()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            const int flightNumber = 67;

            var query = new GetFlightByIdQuery(flightNumber);

            var kievAirport = new Airport
            {
                Id = 97,
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };
            var berlinAirport = new Airport
            {
                Id = 99,
                Name = "Berlin",
                City = "Berlin",
                Country = "Germany"
            };
            var flight = new Flight
            {
                Id = flightNumber,
                ArrivalAirport = kievAirport,
                DepartureAirport = berlinAirport,
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/16 18:00"),
                DepartureTime = DateTime.Parse("22/09/16 13:00"),
                Status = "normal"
            };
            var flightDTO = new FlightDTO
            {
                Id = flight.Id,
                ArrivalAirportName = flight.ArrivalAirport.Name,
                DepartureAirportName = flight.DepartureAirport.Name,
                Terminal = flight.Terminal,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Status = flight.Status,
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByFlightNumber(query, cancellationToken)).ReturnsAsync(flight);
            mockCaster.Setup(f => f.Cast(flight, cancellationToken)).ReturnsAsync(flightDTO);

            var handler = new GetFlightByIdQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = flightDTO;

            // act
            var actualResult = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.SearchByFlightNumber(query, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(flight, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValidAndFlightDoesNotExist_Then_ShouldThrowNotFoundException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightByIdQuery(67);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByFlightNumber(query, cancellationToken))
                .ReturnsAsync(default(Flight));

            var handler = new GetFlightByIdQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<NotFoundException>(() => handler.Handle(query, cancellationToken));
            mockRepository.Verify(f => f.SearchByFlightNumber(query, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryIsinvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var query = new GetFlightByIdQuery(-9);

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.SearchByFlightNumber(query, cancellationToken))
                .ReturnsAsync(default(Flight));

            var handler = new GetFlightByIdQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(query, cancellationToken));
        }
    }
}
