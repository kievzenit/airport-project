using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Passengers.Commands.CreatePassenger;
using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AirportProject.Application.Tests.Passengers.Commands.CreatePassenger
{
    [TestClass]
    public class CreatePassengerCommandValidatorTest
    {
        [TestMethod]
        public void Test_CommandValidator_When_CommandIsValidAndPassengerDoesNotExist_Then_ShouldReturnTrue()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            mockRepository.Setup(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(default(Passenger));

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsTrue(result);
            mockRepository.Verify(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public void Test_CommandValidator_When_CommandIsValidAndPassengerExists_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var existingPassenger = new Passenger
            {
                Id = 987,
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            mockRepository.Setup(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(existingPassenger);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
            mockRepository.Verify(f =>
                f.SearchByPassport(It.IsAny<GetPassengerByPassportQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public void Test_CommandValidator_When_FirstnameIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = new string('a', 51),
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_FirstnameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = string.Empty,
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_LastnameIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = new string('a', 51),
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_LastnameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = string.Empty,
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_PassportIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_NationalityIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = new string('a', 51),
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_NetionalityIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = String.Empty,
                Gender = "female",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_GenderIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "notexisting",
                Birthday = DateTime.Parse("22/07/19")
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_CommandValidator_When_BrthdayIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            var mockRepository = new Mock<IPassengerRepository>(MockBehavior.Strict);

            var command = new CreatePassengerCommand
            {
                Firstname = "Elvira",
                Lastname = "Jacob",
                Passport = "aa991389",
                Nationality = "American",
                Gender = "female",
                Birthday = DateTime.MaxValue
            };

            // act

            var result = command.IsValid(mockRepository.Object);

            // assert
            Assert.IsFalse(result);
        }
    }
}
