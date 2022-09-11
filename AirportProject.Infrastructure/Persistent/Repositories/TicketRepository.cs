using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using AirportProject.Application.Abstract;
using AirportProject.Infrastructure.Persistent.Casting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId;
using System.Threading;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AirportProjectDBContext context;

        public TicketRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<TicketDTO>> GetTickets(TicketDTO ticketDTO)
        {
            var flights = await this.context.Flights
                .Where(f => f.ArrivalAirport.Name == ticketDTO.To && f.DepartureAirport.Name == ticketDTO.From)
                .ToListAsync();

            var tickets = new List<Ticket>();

            foreach (var flight in flights)
            {
                var flightRelatedTickets = await this.context.Tickets
                    .Where(t => t.Flight == flight && t.Type == ticketDTO.Type)
                    .ToListAsync();

                tickets.AddRange(flightRelatedTickets);
            }

            return await tickets.ToTicktDTOs();
        }

        public async Task<ICollection<Ticket>> GetTickets(
            GetTicketsByPassengerIdQuery query, CancellationToken cancellationToken)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Id == query.PassengerId, cancellationToken);

            var tickets = await this.context.PassengersTickets
                .Where(pt => pt.Passenger == passenger)
                .Select(pt => pt.Ticket)
                .ToListAsync(cancellationToken);

            return tickets;
        }
    }
}
