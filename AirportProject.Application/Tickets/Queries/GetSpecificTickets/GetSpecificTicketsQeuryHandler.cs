using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Tickets.Queries.GetSpecificTickets
{
    public class GetSpecificTicketsQeuryHandler :
        IRequestHandler<GetSpecificTicketsQuery, IEnumerable<TicketDTO>>
    {
        private readonly ITicketRepository repository;

        public GetSpecificTicketsQeuryHandler(ITicketRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TicketDTO>> Handle(
            GetSpecificTicketsQuery request, CancellationToken cancellationToken)
        {
            var ticketDTO = new TicketDTO
            {
                From = request.From,
                To = request.To,
                Type = request.Type
            };

            if (!ticketDTO.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            return await this.repository.GetTickets(ticketDTO);
        }
    }
}
