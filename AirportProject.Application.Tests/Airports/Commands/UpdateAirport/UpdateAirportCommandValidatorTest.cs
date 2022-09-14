using AirportProject.Application.Airports.Commands.UpdateAirport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Airports.Commands.UpdateAirport
{
    [TestClass]
    public class UpdateAirportCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_DataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new UpdateAirportCommand
            {
                Id = 89,
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportIdIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateAirportCommand
            {
                Id = -89,
                Name = "Kiev",
                City = "Borispol",
                Country = "Ukraine"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportNameIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateAirportCommand
            {
                Id = 89,
                Name = new string('a', 51),
                City = "Borispol",
                Country = "Ukraine"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportCityIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateAirportCommand
            {
                Id = 89,
                Name = "Kiev",
                City = new string('a', 51),
                Country = "Ukraine"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_AirportCountryIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdateAirportCommand
            {
                Id = 89,
                Name = "Kiev",
                City = "Borispol",
                Country = new string('a', 51)
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
