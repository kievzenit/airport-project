using AirportProject.Domain;
using AirportProject.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    abstract public class AbstractRepository
    {
        protected abstract AirportProjectDBContext context { get; set; }

        protected Task<Airport> AirportDTOToAirport(AirportDTO airportDTO)
        {
            return Task.FromResult(
                new Airport
                {
                    Id = airportDTO.Id,
                    Name = airportDTO.Name,
                    Country = airportDTO.Country,
                    City = airportDTO.City
                });
        }

        protected Task<AirportDTO> AirportToAirportDTO(Airport airport)
        {
            return Task.FromResult(
                new AirportDTO
                {
                    Id = airport.Id,
                    Name = airport.Name,
                    Country = airport.Country,
                    City = airport.City
                });
        }

        protected async Task<List<AirportDTO>> AirportsToAirportDTOs(IEnumerable<Airport> airports)
        {
            var airportDTOs = new List<AirportDTO>();

            foreach (var airport in airports)
            {
                var airportDTO = await this.AirportToAirportDTO(airport);

                airportDTOs.Add(airportDTO);
            }

            return airportDTOs;
        }

        protected async Task<Flight> FlightDTOToFlight(FlightDTO flightDTO)
        {
            var arrivalAirport = await this.context.Airports
                    .FirstOrDefaultAsync(a => a.Name == flightDTO.ArrivalAirportName);

            if (arrivalAirport == null)
            {
                return default;
            }

            var departureAirport = await this.context.Airports
                    .FirstOrDefaultAsync(a => a.Name == flightDTO.DepartureAirportName);

            if (departureAirport == null)
            {
                return default;
            }

            return new Flight
            {
                Id = flightDTO.Id,
                Terminal = flightDTO.Terminal,
                ArrivalTime = flightDTO.ArrivalTime,
                DepartureTime = flightDTO.DepartureTime,
                Status = flightDTO.Status,
                ArrivalAirport = arrivalAirport,
                DepartureAirport = departureAirport
            };
        }

        protected async Task<FlightDTO> FlightToFlightDTO(Flight flight)
        {
            var arrivalAirportName = flight.ArrivalAirport.Name;
            var departureAirportName = flight.DepartureAirport.Name;

            var economyTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == arrivalAirportName
                    && t.Flight.DepartureAirport.Name == departureAirportName
                    && t.Type == "economy");

            var businessTicket = await this.context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == arrivalAirportName
                    && t.Flight.DepartureAirport.Name == departureAirportName
                    && t.Type == "business");

            return new FlightDTO
            {
                Id = flight.Id,
                ArrivalAirportName = arrivalAirportName,
                DepartureAirportName = departureAirportName,
                Terminal = flight.Terminal,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Status = flight.Status,
                EconomyPrice = economyTicket.Price,
                BusinessPrice = businessTicket.Price
            };
        }

        protected async Task<IEnumerable<FlightDTO>> FlightsToFlightDTOs(IEnumerable<Flight> flights)
        {
            var flightDTOs = new List<FlightDTO>();

            foreach (var flight in flights)
            {
                var flightDTO = await this.FlightToFlightDTO(flight);

                flightDTOs.Add(flightDTO);
            }

            return flightDTOs;
        }

        protected async Task<IEnumerable<TicketDTO>> GetTicketDTOs(Passenger passenger)
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

        protected Task<Passenger> PassengerDTOToPassenger(PassengerDTO passengerDTO)
        {
            return Task.FromResult(
                new Passenger
                {
                    Id = passengerDTO.Id,
                    Firstname = passengerDTO.Firstname,
                    Lastname = passengerDTO.Lastname,
                    Passport = passengerDTO.Passport,
                    Nationality = passengerDTO.Nationality,
                    Birthday = passengerDTO.Birthday,
                    Gender = passengerDTO.Gender
                });

        }

        protected Task<PassengerDTO> PassengerToPassengerDTO(Passenger passenger)
        {
            return Task.FromResult(
                new PassengerDTO
                {
                    Id = passenger.Id,
                    Firstname = passenger.Firstname,
                    Lastname = passenger.Lastname,
                    Passport = passenger.Passport,
                    Nationality = passenger.Nationality,
                    Birthday = passenger.Birthday,
                    Gender = passenger.Gender,
                });
        }

        protected async Task<List<PassengerDTO>> PassengersToPassengerDTOs(List<Passenger> passengers)
        {
            var passengerDTOs = new List<PassengerDTO>();

            foreach (var passenger in passengers)
            {
                var passengerDTO = await this.PassengerToPassengerDTO(passenger);

                passengerDTOs.Add(passengerDTO);
            }

            return passengerDTOs;
        }

        protected Task<TicketDTO> TicketToTicketDTO(Ticket ticket)
        {
            return Task.FromResult(
                new TicketDTO
                {
                    From = ticket.Flight.DepartureAirport.Name,
                    To = ticket.Flight.ArrivalAirport.Name,
                    Type = ticket.Type,
                    Id = ticket.Id,
                    FlightId = ticket.FlightId,
                    ArrivalTime = ticket.Flight.ArrivalTime,
                    DepartureTime = ticket.Flight.DepartureTime,
                    Price = ticket.Price
                });
        }

        protected async Task<IEnumerable<TicketDTO>> TicketsToTicktDTOs(IEnumerable<Ticket> tickets)
        {
            var ticketDTOs = new List<TicketDTO>();

            foreach (var ticket in tickets)
            {
                var ticketDTO = await this.TicketToTicketDTO(ticket);

                ticketDTOs.Add(ticketDTO);
            }

            return ticketDTOs;
        }
    }
}
