using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Tickets.Queries.GetSpecificTickets
{
    public class GetSpecificTicketsQueryHandler :
        IRequestHandler<GetSpecificTicketsQuery, IEnumerable<TicketDTO>>
    {
        private readonly ITicketRepository repository;
        private readonly ICaster<Ticket, TicketDTO> caster;

        public GetSpecificTicketsQueryHandler(ITicketRepository repository, ICaster<Ticket, TicketDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<IEnumerable<TicketDTO>> Handle(
            GetSpecificTicketsQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var tickets = await this.repository.GetTickets(request, cancellationToken);

            return await this.caster.Cast(tickets, cancellationToken);
        }
    }
}
