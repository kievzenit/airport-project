using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using AirportProject.Application.Abstract;
using AirportProject.Infrastructure.Persistent.Casting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ICollection<TicketDTO>> GetTickets(int passengerId)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Id == passengerId);

            if (passenger == null)
            {
                return default;
            }

            return await this.GetTicketDTOs(passenger);
        }

        private async Task<ICollection<TicketDTO>> GetTicketDTOs(Passenger passenger)
        {
            var tickets = await this.context.PassengersTickets
                .Where(pt => pt.Passenger == passenger)
                .Select(pt => pt.Ticket)
                .ToListAsync();

            var ticketDTOs = new List<TicketDTO>();

            foreach (var ticket in tickets)
            {
                var ticketDTO = new TicketDTO
                {
                    From = ticket.Flight.DepartureAirport.Name,
                    To = ticket.Flight.ArrivalAirport.Name,
                    Type = ticket.Type,
                    Id = ticket.Id,
                    FlightId = ticket.FlightId,
                    ArrivalTime = ticket.Flight.ArrivalTime,
                    DepartureTime = ticket.Flight.DepartureTime,
                    Price = ticket.Price
                };

                ticketDTOs.Add(ticketDTO);
            }

            return ticketDTOs;
        }
    }
}
