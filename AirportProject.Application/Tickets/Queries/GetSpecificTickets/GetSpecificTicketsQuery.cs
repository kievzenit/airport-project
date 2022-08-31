using AirportProject.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AirportProject.Application.Tickets.Queries.GetSpecificTickets
{
    public record GetSpecificTicketsQuery : IRequest<IEnumerable<TicketDTO>>
    {
        public string From { get; init; }
        public string To { get; init; }
        public string Type { get; init; }
    }
}
