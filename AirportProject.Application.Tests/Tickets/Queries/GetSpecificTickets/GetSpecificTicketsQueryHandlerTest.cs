using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Tickets.Queries.GetSpecificTickets;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AirportProject.Application.Tests.Tickets.Queries.GetSpecificTickets
{
    [TestClass]
    public class GetSpecificTicketsQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldFinishedWithoutExceptions()
        {
            // arrange
            var mockRepository = new Mock<ITicketRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Ticket, TicketDTO>>(MockBehavior.Strict);

            var query = new GetSpecificTicketsQuery
            {
                From = "Kiev",
                To = "Berlin",
                Type = "economy"
            };
            var tickets = new List<Ticket>();
            var ticketDTOs = new List<TicketDTO>();

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.GetTickets(query, cancellationToken)).ReturnsAsync(tickets);
            mockCaster.Setup(f => f.Cast(tickets, cancellationToken)).ReturnsAsync(ticketDTOs);

            var handler = new GetSpecificTicketsQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = ticketDTOs;

            // act
            var actualResult = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.GetTickets(query, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(tickets, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_QueryIsInvalid_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<ITicketRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Ticket, TicketDTO>>(MockBehavior.Strict);

            var query = new GetSpecificTicketsQuery
            {
                From = new string('a', 51),
                To = string.Empty,
                Type = "doesnotexisting"
            };
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetSpecificTicketsQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }
    }
}
