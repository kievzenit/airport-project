using AirportProject.Application.Passengers.Commands.DeletePassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Commands.DeletePassenger
{
    [TestClass]
    public class DeletePassengerCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_CommandIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new DeletePassengerCommand(4);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_IdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new DeletePassengerCommand(-4);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_IdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new DeletePassengerCommand(0);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
