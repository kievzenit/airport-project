using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId;
using AirportProject.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AirportProject.Application.Tests.Tickets.Queries.GetTicketsByPassengerId
{
    [TestClass]
    public class GetTicketsByPassengerIdQueryHandlerTest
    {
        [TestMethod]
        public void Test_QueryHandler_When_QueryIsValid_Then_ShouldReturnICollectionOfPassengerDTO()
        {
            // arrange
            var mockRepository = new Mock<ITicketRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Ticket, TicketDTO>>(MockBehavior.Strict);

            var query = new GetTicketsByPassengerIdQuery(5);
            var tickets = new List<Ticket>();
            var ticketDTOs = new List<TicketDTO>();

            var cancellationToken = new CancellationTokenSource().Token;

            mockRepository.Setup(f => f.GetTickets(query, cancellationToken)).ReturnsAsync(tickets);
            mockCaster.Setup(f => f.Cast(tickets, cancellationToken)).ReturnsAsync(ticketDTOs);

            var handler = new GetTicketsByPassengerIdQueryHandler(mockRepository.Object, mockCaster.Object);

            var expectedResult = ticketDTOs;

            // act
            var actualResult = handler.Handle(query, cancellationToken).Result;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
            mockRepository.Verify(f => f.GetTickets(query, cancellationToken), Times.Once);
            mockCaster.Verify(f => f.Cast(tickets, cancellationToken), Times.Once);
        }

        [TestMethod]
        public void Test_QueryHandler_When_PassengerIdIsNegative_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<ITicketRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Ticket, TicketDTO>>(MockBehavior.Strict);

            var query = new GetTicketsByPassengerIdQuery(-5);
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetTicketsByPassengerIdQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }

        [TestMethod]
        public void Test_QueryHandler_When_PassengerIdIsZero_Then_ShouldThrowArgumentException()
        {
            // arrange
            var mockRepository = new Mock<ITicketRepository>(MockBehavior.Strict);
            var mockCaster = new Mock<ICaster<Ticket, TicketDTO>>(MockBehavior.Strict);

            var query = new GetTicketsByPassengerIdQuery(0);
            var cancellationToken = new CancellationTokenSource().Token;
            var handler = new GetTicketsByPassengerIdQueryHandler(mockRepository.Object, mockCaster.Object);

            // assert
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => handler.Handle(query, cancellationToken))
                .Wait();
        }
    }
}
