using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Casting;
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
        private readonly TicketsCaster caster;

        public GetTicketsByPassengerIdQueryHandler(ITicketRepository repository, TicketsCaster caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<IEnumerable<TicketDTO>> Handle(
            GetTicketsByPassengerIdQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Passenger id must be not equal or less than zero");
            }

            var tickets = await this.repository.GetTickets(request, cancellationToken);

            return await this.caster.Cast(tickets);
        }
    }
}
