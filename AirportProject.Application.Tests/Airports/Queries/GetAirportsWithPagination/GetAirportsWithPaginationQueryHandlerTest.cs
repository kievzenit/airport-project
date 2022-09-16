using AirportProject.Application.Airports.Queries.GetAirportsWithPagination;
using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Tests.Airports.Queries.GetAirportsWithPagination
{
    [TestClass]
    public class GetAirportsWithPaginationQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnValidPageResultDTO()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Airport, AirportDTO>>(MockBehavior.Strict);
            
            var query = new GetAirportsWithPaginationQuery(5);
            var airports = (ICollection<Airport>)new List<Airport>();
            var airportsDTO = (ICollection<AirportDTO>)new List<AirportDTO>();

            const int totalAirportsCount = 50;

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            mockRepository.Setup(f => f.GetRange(query.PageNumber, query.PageSize, cancellationToken))
                .ReturnsAsync(airports);
            mockRepository.Setup(f => f.GetTotalCount(cancellationToken)).ReturnsAsync(totalAirportsCount);
            mockCaster.Setup(f => f.Cast(airports, cancellationToken)).ReturnsAsync(airportsDTO);

            var handler = new GetAirportsWithPaginationQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedItems = airportsDTO;
            var expectedCount = totalAirportsCount;

            // act
            var result = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedItems, result.Items);
            Assert.AreEqual(expectedCount, result.TotalCount);

            mockRepository.Verify(f => 
                f.GetRange(query.PageNumber, query.PageSize, cancellationToken), 
                Times.Once);
            mockRepository.Verify(f => f.GetTotalCount(cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(airports, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<IAirportRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Airport, AirportDTO>>(MockBehavior.Strict);

            var query = new GetAirportsWithPaginationQuery(-5);

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            var handler = new GetAirportsWithPaginationQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(() => handler.Handle(query, cancellationToken)).Wait();
        }
    }
}
