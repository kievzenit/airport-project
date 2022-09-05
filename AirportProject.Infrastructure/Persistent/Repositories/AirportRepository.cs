using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using AirportProject.Application.Abstract;
using AirportProject.Infrastructure.Persistent.Casting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AirportProject.Application.Airports.Commands.CreateAirport;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportProjectDBContext context;

        public AirportRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<Airport> Create(
            CreateAirportCommand createAirportCommand, CancellationToken cancellationToken)
        {
            var airport = await createAirportCommand.ToAirport();

            await this.context.AddAsync(airport, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return airport;
        }

        public async Task<bool> Delete(int id)
        {
            var airport = await this.context.Airports
                .FirstOrDefaultAsync(a => a.Id == id);

            if (airport == null)
                return false;

            var flights = await this.context.Flights
                .Where(f => f.ArrivalAirport == airport || f.DepartureAirport == airport)
                .ToListAsync();

            var tickets = new List<Ticket>();
            foreach (var flight in flights)
            {
                var relatedTickets = await this.context.Tickets
                    .Where(t => t.Flight == flight)
                    .ToListAsync();

                tickets.AddRange(relatedTickets);
            }

            var passengersTickets = new List<PassengersTickets>();
            foreach (var ticket in tickets)
            {
                var relatedPassengersTickets = await this.context.PassengersTickets
                    .Where(pt => pt.Ticket == ticket)
                    .ToListAsync();

                this.context.RemoveRange(relatedPassengersTickets);
            }

            this.context.RemoveRange(tickets);
            this.context.RemoveRange(flights);
            this.context.Remove(airport);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Airport>> GetRange(
            int offset, int count, CancellationToken cancellationToken)
        {
            var airports = await this.context.Airports
                .Skip((offset - 1) * count)
                .Take(count)
                .ToListAsync(cancellationToken);

            return airports;
        }

        public async Task<bool> Update(AirportDTO airportDTO)
        {
            var airport = await this.context.Airports
                .FirstOrDefaultAsync(a => a.Id == airportDTO.Id);

            if (airport == null)
                return false;

            airport.Name = airportDTO.Name;
            airport.Country = airportDTO.Country;
            airport.City = airportDTO.City;

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            var airports = await this.context.Airports.ToListAsync(cancellationToken);

            return airports.Count;
        }

        public async Task<bool> DoesAirportExists(string name, CancellationToken cancellationToken)
        {
            var airport = await this.context.Airports
                .FirstOrDefaultAsync(a => a.Name == name, cancellationToken);

            return airport != null;
        }
    }
}
