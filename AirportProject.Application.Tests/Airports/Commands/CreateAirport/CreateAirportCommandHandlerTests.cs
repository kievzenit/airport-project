using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Airports.Commands.CreateAirport
{
    [TestClass]
    public class CreateAirportCommandHandlerTests
    {
        [TestMethod]
        public void Test_HandleMethod_ShouldReturnAirportDTO()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Airport, AirportDTO>>(MockBehavior.Strict);

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
            mockRepository.Setup(f => f.DoesAirportExists(command.Name, cancellationToken)).ReturnsAsync(false);
            mockCaster.Setup(f => f.Cast(airport, cancellationToken)).ReturnsAsync(airportDTO);

            var handler = new CreateAirportCommandHandler(mockRepository.Object, mockCaster.Object);

            var expected = airportDTO;

            // act
            var actual = handler.Handle(command, cancellationToken).Result;

            // assert
            Assert.AreEqual(expected, actual);
            mockRepository.Verify(f => f.Create(command, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_HandleMethod_When_InputDataIsEmpty_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>();
            var mockCaster = new Mock<ICaster<Airport, AirportDTO>>();

            var command = new CreateAirportCommand
            {
                Name = "",
                City = "",
                Country = ""
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var handler = new CreateAirportCommandHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken));

            mockRepository.Verify(f =>
                f.Create(It.IsAny<CreateAirportCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
            mockRepository.Verify(f =>
                f.DoesAirportExists(It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [TestMethod]
        public void Test_HandleMethod_When_InputDataIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Airport, AirportDTO>>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = new string('a', 51),
                City = new string('a', 51),
                Country = new string('a', 51)
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var handler = new CreateAirportCommandHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(command, cancellationToken));
        }
    }
}
