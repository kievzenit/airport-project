using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Flights.Commands.CreateFlight;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Flights.Commands.CreateFlight
{
    [TestClass]
    public class CreateFlightCommandHandlerTest
    {
        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsValid_Then_ShouldReturnFlightDTO()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var kievAirport = new Airport
            {
                Id = 8,
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };
            var berlinAirport = new Airport
            {
                Id = 19,
                Name = "Berlin",
                City = "Berlin",
                Country = "Germany"
            };

            var command = new CreateFlightCommand
            {
                ArrivalAirportName = kievAirport.Name,
                DepartureAirportName = berlinAirport.Name,
                Terminal = 'G',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("2/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            var flight = new Flight
            {
                Id = 289,
                ArrivalAirport = kievAirport,
                DepartureAirport = berlinAirport,
                Terminal = command.Terminal,
                ArrivalTime = command.ArrivalTime,
                DepartureTime = command.DepartureTime,
                Status = command.Status
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
                EconomyPrice = command.EconomyPrice,
                BusinessPrice = command.BusinessPrice
            };

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.Create(command, cancellationToken)).ReturnsAsync(flight);
            mockCaster.Setup(f => f.Cast(flight, cancellationToken)).ReturnsAsync(flightDTO);

            var handler = new CreateFlightCommandHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = flightDTO;

            // act
            var actualResult = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.Create(command, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(flight, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandHandler_When_InputDataIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IFlightRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Flight, FlightDTO>>(MockBehavior.Strict);

            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = '8',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("2/09/15 09:00"),
                Status = "notexisting",
                EconomyPrice = 10,
                BusinessPrice = 1200
            };

            var cancellationToken = new CancellationTokenSource().Token;

            var handler = new CreateFlightCommandHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken)).Wait();
        }
    }
}
