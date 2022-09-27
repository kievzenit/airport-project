using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengerByPassport
{
    [TestClass]
    public class GetPassengerByPassportQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_CommandIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            const string passport = "ad239916";
            var query = new GetPassengerByPassportQuery(passport);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_CommandIsInvalid_Then_ShouldReturnFalse()
        {
            // arrange
            const string passport = "hhjg9922";
            var query = new GetPassengerByPassportQuery(passport);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PassportIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            const string passport = "";
            var query = new GetPassengerByPassportQuery(passport);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PassportIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            const string passport = "aa345679dd";
            var query = new GetPassengerByPassportQuery(passport);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_PassportIsTooSmall_Then_ShouldReturnFalse()
        {
            // arrange
            const string passport = "aa34";
            var query = new GetPassengerByPassportQuery(passport);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
