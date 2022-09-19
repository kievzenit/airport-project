using AirportProject.Application.Passengers.Commands.UpdatePassenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Commands.UpdatePassenger
{
    [TestClass]
    public class UpdatePassengerCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_CommandIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_IdIsNegative_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = -98,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_IdIsZero_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 0,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_FirstnameIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = new string('a', 51),
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_FirstnameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = string.Empty,
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_LastnameIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = new string('a', 51),
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_LastnameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = string.Empty,
                Passport = "aa991389",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_PassportIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa",
                Nationality = "American"
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_NationalityIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = new string('a', 51)
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_NationalityIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var command = new UpdatePassengerCommand
            {
                Id = 98,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = string.Empty
            };

            // act
            var result = command.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
