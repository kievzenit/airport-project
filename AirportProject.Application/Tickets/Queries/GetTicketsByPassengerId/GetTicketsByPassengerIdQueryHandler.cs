using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId
{
    public class GetTicketsByPassengerIdQueryHandler :
        IRequestHandler<GetTicketsByPassengerIdQuery, IEnumerable<TicketDTO>>
    {
        private readonly ITicketRepository repository;

        public GetTicketsByPassengerIdQueryHandler(ITicketRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TicketDTO>> Handle(
            GetTicketsByPassengerIdQuery request, CancellationToken cancellationToken)
        {
            if (request.PassengerId <= 0)
            {
                throw new ArgumentException("Passenger id must be not equal or less than zero");
            }

            return await this.repository.GetTickets(request.PassengerId);
        }
    }
}
