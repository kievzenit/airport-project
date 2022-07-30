using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using AirportProject.Infrastructure.Persistent.Abstract;
using AirportProject.Infrastructure.Persistent.Casting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportProjectDBContext context;

        public AirportRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<AirportDTO> Create(AirportDTO airportDTO)
        {
            var airport = await airportDTO.ToAirport();

            await this.context.AddAsync(airport);
            await this.context.SaveChangesAsync();

            airportDTO.Id = airport.Id;

            return airportDTO;
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

        public async Task<ICollection<AirportDTO>> GetAll()
        {
            var airports = await this.context.Airports.ToListAsync();

            return await airports.ToAirportDTOs();
        }

        public async Task<ICollection<AirportDTO>> GetRange(int offset, int count)
        {
            var airports = await this.context.Airports
                .Skip((offset - 1) * count)
                .Take(count)
                .ToListAsync();

            return await airports.ToAirportDTOs();
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

        public async Task<int> GetTotalCount()
        {
            var airports = await this.context.Airports.ToListAsync();

            return airports.Count;
        }
    }
}
