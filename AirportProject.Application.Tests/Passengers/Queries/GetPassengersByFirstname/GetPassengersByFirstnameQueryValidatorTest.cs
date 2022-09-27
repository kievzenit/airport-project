using AirportProject.Application.Passengers.Queries.GetPassengersByFirstname;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengersByFirstname
{
    [TestClass]
    public class GetPassengersByFirstnameQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetPassengersByFirstnameQuery("Leo");

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_FirstnameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetPassengersByFirstnameQuery(string.Empty);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_FirstnameIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetPassengersByFirstnameQuery(new string('a', 51));

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
