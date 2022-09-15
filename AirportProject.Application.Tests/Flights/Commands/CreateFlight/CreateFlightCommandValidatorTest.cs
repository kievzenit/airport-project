using AirportProject.Application.Flights.Commands.CreateFlight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AirportProject.Application.Tests.Flights.Commands.CreateFlight
{
    [TestClass]
    public class CreateFlightCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_DataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_ArrivalAirportNameIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = new string('a', 51),
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_ArrivalAirportNameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = string.Empty,
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_DepartureAirportNameIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = new string('a', 51),
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_DepartureAirportNameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = string.Empty,
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_TerminalIsNotValidCharacter_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = '6',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_ArrivalTimeIsEqualToDepartureTime_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 15:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_ArrivalTimeIsLessThanToDepartureTime_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 10:00"),
                DepartureTime = DateTime.Parse("22/09/15 15:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_StatusIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "notexisting",
                EconomyPrice = 1000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_EconomyPriceIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000000,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_EconomyPriceIsTooSmall_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 10,
                BusinessPrice = 1200
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_BusinessPriceIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 1200000
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_BusinessPriceIsTooSmall_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new CreateFlightCommand
            {
                ArrivalAirportName = "Kiev",
                DepartureAirportName = "Berlin",
                Terminal = 'K',
                ArrivalTime = DateTime.Parse("22/09/15 15:00"),
                DepartureTime = DateTime.Parse("22/09/15 09:00"),
                Status = "normal",
                EconomyPrice = 1000,
                BusinessPrice = 12
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
