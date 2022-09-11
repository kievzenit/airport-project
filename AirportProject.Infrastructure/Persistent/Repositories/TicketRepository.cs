using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Tickets.Queries.GetSpecificTickets;
using AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId;
using AirportProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AirportProjectDBContext context;

        public TicketRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Ticket>> GetTickets(
            GetSpecificTicketsQuery query, CancellationToken cancellationToken)
        {
            var flights = await this.context.Flights
                .Where(f => f.ArrivalAirport.Name == query.To 
                         && f.DepartureAirport.Name == query.From)
                .ToListAsync(cancellationToken);

            var tickets = new List<Ticket>();

            foreach (var flight in flights)
            {
                var flightRelatedTickets = await this.context.Tickets
                    .Where(t => t.Flight == flight && t.Type == query.Type)
                    .ToListAsync(cancellationToken);

                tickets.AddRange(flightRelatedTickets);
            }

            return tickets;
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
