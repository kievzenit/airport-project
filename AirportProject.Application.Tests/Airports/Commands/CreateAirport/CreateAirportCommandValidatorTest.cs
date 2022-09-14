using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Application.Common.Abstract;
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
    public class CreateAirportCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_DataIsValidAndAirportWithThisNameDoesNotExist_ShouldReturnTrue()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.DoesAirportExists(command.Name, cancellationToken)).ReturnsAsync(false);

            // act
            var result = command.IsValid(mockRepository.Object, cancellationToken).Result;

            // assert
            Assert.IsTrue(result);
            mockRepository.Verify(f => f.DoesAirportExists(command.Name, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandValidator_When_DataIsValidAndAirportWithThisNameDoesExist_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.DoesAirportExists(command.Name, cancellationToken)).ReturnsAsync(true);

            // act
            var result = command.IsValid(mockRepository.Object, cancellationToken).Result;

            // assert
            Assert.IsFalse(result);
            mockRepository.Verify(f => f.DoesAirportExists(command.Name, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportNameIsInvalid_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = new string('a', 51),
                City = "Borispol",
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            // act
            var result = command.IsValid(mockRepository.Object, cancellationToken).Result;

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportCityIsInvalid_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = "Kiev",
                City = new string('a', 51),
                Country = "Ukraine"
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            // act
            var result = command.IsValid(mockRepository.Object, cancellationToken).Result;

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportCountryIsInvalid_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = "Kiev",
                City = "Borispol",
                Country = new string('a', 51)
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            // act
            var result = command.IsValid(mockRepository.Object, cancellationToken).Result;

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_DataIsInvalid_ShouldNotCallRepositoryDoesAirportExistsMethod()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);

            var command = new CreateAirportCommand
            {
                Name = new string('a', 51),
                City = new string('a', 51),
                Country = new string('a', 51)
            };

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.DoesAirportExists(command.Name, cancellationToken)).ReturnsAsync(false);

            // act
            var result = command.IsValid(mockRepository.Object, cancellationToken).Result;

            // assert
            mockRepository.Verify(f => f.DoesAirportExists(command.Name, cancellationToken), Times.Never);
        }
    }
}
