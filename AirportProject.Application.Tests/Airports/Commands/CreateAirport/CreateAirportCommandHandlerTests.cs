using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Tests.Airports.Commands.CreateAirport
{
    [TestClass]
    public class CreateAirportCommandHandlerTests
    {
        [TestMethod]
        public void Test_HandleMethod_Should_ReturnAirportDTO()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>();
            var mockCaster = new Mock<ICaster<Airport, AirportDTO>>();

            var command = new CreateAirportCommand
            {
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };
            var airport = new Airport
            {
                Id = 1,
                Name = command.Name,
                City = command.City,
                Country = command.Country
            };
            var airportDTO = new AirportDTO
            {
                Id = airport.Id,
                Name = airport.Name,
                City = airport.City,
                Country = airport.Country
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.Create(command, cancellationToken)).ReturnsAsync(airport);
            mockCaster.Setup(f => f.Cast(airport, cancellationToken)).ReturnsAsync(airportDTO);

            var handler = new CreateAirportCommandHandler(mockRepository.Object, mockCaster.Object);

            var expected = airportDTO;

            // act
            var actual = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.AreEqual(expected, actual);
            mockRepository.Verify(f => f.Create(command, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(airport, cancellationToken), Times.Once);
        }
    }
}
