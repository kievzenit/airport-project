using AirportProject.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId
{
    public record GetTicketsByPassengerIdQuery(int passengerId) : IRequest<IEnumerable<TicketDTO>>
    {
        public int PassengerId { get; init; } = passengerId;
    }
}
