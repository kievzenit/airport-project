using AirportProject.Domain;
using AirportProject.DTOs;
using AirportProject.Infrastructure.Persistent.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class TicketRepository : AbstractRepository, ITicketRepository
    {
        private AirportProjectDBContext dBContext;

        protected override AirportProjectDBContext context
        {
            get => this.dBContext;
            set => this.dBContext = value;
        }

        public TicketRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TicketDTO>> GetTickets(TicketDTO ticketDTO)
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

            return await this.TicketsToTicktDTOs(tickets);
        }

        public async Task<IEnumerable<TicketDTO>> GetTickets(int passengerId)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Id == passengerId);

            if (passenger == null)
            {
                return default;
            }

            return await this.GetTicketDTOs(passenger);
        }
    }
}
