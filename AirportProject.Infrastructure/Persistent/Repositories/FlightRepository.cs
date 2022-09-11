using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Flights.Commands.CreateFlight;
using AirportProject.Application.Flights.Commands.DeleteFlight;
using AirportProject.Application.Flights.Commands.UpdateFlight;
using AirportProject.Application.Flights.Queries.GetFlightById;
using AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport;
using AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport;
using AirportProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task<Flight> Create(CreateFlightCommand command, CancellationToken cancellationToken)
        {
            var arrivalAirport = await this.context.Airports.FirstOrDefaultAsync(
                a => a.Name == command.ArrivalAirportName, cancellationToken);
            var departureAirport = await this.context.Airports.FirstOrDefaultAsync(
                a => a.Name == command.DepartureAirportName, cancellationToken);

            if (arrivalAirport == null || departureAirport == null)
                return default;


            var flight = new Flight
            {
                ArrivalAirport = arrivalAirport,
                DepartureAirport = departureAirport,
                ArrivalTime = command.ArrivalTime,
                DepartureTime = command.DepartureTime,
                Status = command.Status,
                Terminal = command.Terminal
            };

            var economyTicket = new Ticket
            {
                Price = command.EconomyPrice,
                Type = "economy",
                Flight = flight
            };

            var businessTicket = new Ticket
            {
                Price = command.BusinessPrice,
                Type = "business",
                Flight = flight
            };

            await this.context.AddAsync(flight, cancellationToken);
            await this.context.AddAsync(economyTicket, cancellationToken);
            await this.context.AddAsync(businessTicket, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            if (economyTicket.Id <= 0 || businessTicket.Id <= 0)
                return default;

            return flight;
        }

        public async Task<bool> Delete(DeleteFlightCommand command, CancellationToken cancellationToken)
        {
            var flight = await this.context.Flights
                .FirstOrDefaultAsync(f => f.Id == command.Id, cancellationToken);

            if (flight == null)
                return false;

            var tickets = await this.context.Tickets
                .Where(t => t.Flight == flight)
                .ToListAsync(cancellationToken);

            foreach (var ticket in tickets)
            {
                var relatedPassengersTickets = await this.context.PassengersTickets
                    .Where(pt => pt.Ticket == ticket)
                    .ToListAsync(cancellationToken);

                this.context.RemoveRange(relatedPassengersTickets);
            }

            this.context.RemoveRange(tickets);
            this.context.Remove(flight);
            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<Flight>> GetRange(
            int offset, int count, CancellationToken cancellationToken)
        {
            var flights = await this.context.Flights
                .Skip((offset - 1) * count)
                .Take(count)
                .ToListAsync(cancellationToken);

            return flights;
        }

        public async Task<bool> Update(UpdateFlightCommand command, CancellationToken cancellationToken)
        {
            var flight = await this.context.Flights
                .FirstOrDefaultAsync(f => f.Id == command.Id, cancellationToken);

            if (flight == null)
                return false;

            flight.Terminal = command.Terminal;
            flight.Status = command.Status;
            flight.DepartureTime = command.DepartureTime;
            flight.ArrivalTime = command.ArrivalTime;

            var economyTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == flight.ArrivalAirport.Name
                    && t.Flight.DepartureAirport.Name == flight.DepartureAirport.Name
                    && t.Type == "economy",
                    cancellationToken);

            if (economyTicket == null)
                return false;

            var businessTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == flight.ArrivalAirport.Name
                    && t.Flight.DepartureAirport.Name == flight.DepartureAirport.Name
                    && t.Type == "business",
                    cancellationToken);

            if (businessTicket == null)
                return false;

            economyTicket.Price = command.EconomyPrice;
            businessTicket.Price = command.BusinessPrice;

            var arrivalAirport = await this.context.Airports
                .FirstOrDefaultAsync(a => a.Name == command.ArrivalAirportName, cancellationToken);

            if (arrivalAirport == null)
                return false;

            flight.ArrivalAirport = arrivalAirport;

            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<Flight>> SearchByFlightArrivalAirport(
            GetFlightsByArrivalAirportQuery query, CancellationToken cancellationToken)
        {
            var flights = await this.context.Flights
                .Where(f => f.ArrivalAirport.Name == query.AirportName)
                .ToListAsync(cancellationToken);

            return flights;
        }

        public async Task<ICollection<Flight>> SearchByFlightDepartureAirport(
            GetFlightsByDepartureAirportQuery query, CancellationToken cancellationToken)
        {
            var flights = await this.context.Flights
                .Where(f => f.DepartureAirport.Name == query.AirportName)
                .ToListAsync(cancellationToken);

            return flights;
        }

        public async Task<Flight> SearchByFlightNumber(
            GetFlightByIdQuery query, CancellationToken cancellationToken)
        {
            var flight = await this.context.Flights
                .FirstOrDefaultAsync(f => f.Id == query.Id, cancellationToken);

            return flight;
        }

        public async Task<Tuple<Ticket, Ticket>> GetTicketsByFlight(
            Flight flight, CancellationToken cancellationToken)
        {
            var economyTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t => t.Flight == flight && t.Type == "economy", cancellationToken);

            var businessTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t => t.Flight == flight && t.Type == "business", cancellationToken);

            return Tuple.Create(economyTicket, businessTicket);
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            return await this.context.Flights.CountAsync(cancellationToken);
        }
    }
}
