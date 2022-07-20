using AirportProject.Domain;
using AirportProject.DTOs;
using AirportProject.Infrastructure.Persistent.Abstract;
using AirportProject.Infrastructure.Persistent.Casting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirportProjectDBContext context;

        public FlightRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<FlightDTO> Create(FlightDTO flightDTO)
        {
            var flight = await flightDTO.ToFlight(this.context);

            if (flight == null)
                return default;

            var economyTicket = new Ticket
            {
                Price = flightDTO.EconomyPrice,
                Type = "economy",
                Flight = flight
            };

            var businessTicket = new Ticket
            {
                Price = flightDTO.BusinessPrice,
                Type = "business",
                Flight = flight
            };

            await this.context.AddAsync(flight);
            await this.context.AddAsync(economyTicket);
            await this.context.AddAsync(businessTicket);
            await this.context.SaveChangesAsync();

            if (economyTicket.Id <= 0 || businessTicket.Id <= 0)
                return default;

            flightDTO.Id = flight.Id;

            return flightDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var flight = await this.context.Flights
                .FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null)
                return false;

            var tickets = await this.context.Tickets
                .Where(t => t.Flight == flight)
                .ToListAsync();

            foreach (var ticket in tickets)
            {
                var relatedPassengersTickets = await this.context.PassengersTickets
                    .Where(pt => pt.Ticket == ticket)
                    .ToListAsync();

                this.context.RemoveRange(relatedPassengersTickets);
            }

            this.context.RemoveRange(tickets);
            this.context.Remove(flight);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<FlightDTO>> GetAll()
        {
            var flights = await this.context.Flights.ToListAsync();

            return await flights.ToFlightDTOs(this.context);
        }

        public async Task<IEnumerable<FlightDTO>> GetRange(int offset, int count)
        {
            var flights = await this.context.Flights
                .Skip((offset - 1) * count)
                .Take(count)
                .ToListAsync();

            return await flights.ToFlightDTOs(this.context);
        }

        public async Task<bool> Update(FlightDTO flightDTO)
        {
            var flight = await this.context.Flights.FirstOrDefaultAsync(f => f.Id == flightDTO.Id);

            if (flight == null)
                return false;

            flight.Terminal = flightDTO.Terminal;
            flight.Status = flightDTO.Status;
            flight.DepartureTime = flightDTO.DepartureTime;
            flight.ArrivalTime = flightDTO.ArrivalTime;

            var economyTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == flight.ArrivalAirport.Name
                    && t.Flight.DepartureAirport.Name == flight.DepartureAirport.Name
                    && t.Type == "economy");

            if (economyTicket == null)
                return false;

            var businessTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == flight.ArrivalAirport.Name
                    && t.Flight.DepartureAirport.Name == flight.DepartureAirport.Name
                    && t.Type == "business");

            if (businessTicket == null)
                return false;

            economyTicket.Price = flightDTO.EconomyPrice;
            businessTicket.Price = flightDTO.BusinessPrice;

            var arrivalAirport = await this.context.Airports
                .FirstOrDefaultAsync(a => a.Name == flightDTO.ArrivalAirportName);

            if (arrivalAirport == null)
                return false;

            flight.ArrivalAirport = arrivalAirport;

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
