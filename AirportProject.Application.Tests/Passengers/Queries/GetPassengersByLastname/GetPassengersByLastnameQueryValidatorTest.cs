using AirportProject.Application.Passengers.Queries.GetPassengersByLastname;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AirportProject.Application.Tests.Passengers.Queries.GetPassengersByLastname
{
    [TestClass]
    public class GetPassengersByLastnameQueryValidatorTest
    {
        [TestMethod]
        public void Test_QueryValidator_When_QueryIsValid_Then_ShouldReturnTrue()
        {
            // arrange
            var query = new GetPassengersByLastnameQuery("Leo");

            // act
            var result = query.IsValid();

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_LastnameIsEmpty_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetPassengersByLastnameQuery(string.Empty);

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_QueryValidator_When_LastnameIsTooLong_Then_ShouldReturnFalse()
        {
            // arrange
            var query = new GetPassengersByLastnameQuery(new string('a', 51));

            // act
            var result = query.IsValid();

            // assert
            Assert.IsFalse(result);
        }
    }
}
