using AirportProject.Application.Airports.Commands.DeleteAirport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportProject.Application.Tests.Airports.Commands.DeleteAirport
{
    [TestClass]
    public class DeleteAirportCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_DataIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new DeleteAirportCommand(5);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_DataIsInValid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new DeleteAirportCommand(-5);

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
