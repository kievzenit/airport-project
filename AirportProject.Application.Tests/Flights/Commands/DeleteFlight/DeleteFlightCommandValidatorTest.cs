using AirportProject.Application.Flights.Commands.DeleteFlight;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Flights.Commands.DeleteFlight
{
    [TestClass]
    public class DeleteFlightCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_DataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new DeleteFlightCommand(87);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_DataIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new DeleteFlightCommand(-87);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_IdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new DeleteFlightCommand(0);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
