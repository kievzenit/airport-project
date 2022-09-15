using AirportProject.Application.Flights.Commands.UpdateFlight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AirportProject.Application.Tests.Flights.Commands.UpdateFlight
{
    [TestClass]
    public class UpdateFlightCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_DataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
        public void Test_commandValidator_When_IdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateFlightCommand
            {
                Id = -872,
                ArrivalAirportName = "Kiev",
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
        public void Test_commandValidator_When_IdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateFlightCommand
            {
                Id = 0,
                ArrivalAirportName = "Kiev",
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
        public void Test_CommandValidator_When_ArrivalAirportNameIsTooBig_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = new string('a', 51),
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = string.Empty,
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
            var command = new UpdateFlightCommand
            {
                Id = 872,
                ArrivalAirportName = "Kiev",
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
