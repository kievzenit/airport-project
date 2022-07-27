using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Casting
{
    public static class FlightCasting
    {
        public static async Task<Flight> ToFlight(this FlightDTO flightDTO, AirportProjectDBContext context)
        {
            var arrivalAirport = await context.Airports
                    .FirstOrDefaultAsync(a => a.Name == flightDTO.ArrivalAirportName);

            if (arrivalAirport == null)
            {
                return default;
            }

            var departureAirport = await context.Airports
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

        public static async Task<FlightDTO> ToFlightDTO(this Flight flight, AirportProjectDBContext context)
        {
            var arrivalAirportName = flight.ArrivalAirport.Name;
            var departureAirportName = flight.DepartureAirport.Name;

            var economyTicket = await context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Flight == flight
                    && t.Flight.ArrivalAirport.Name == arrivalAirportName
                    && t.Flight.DepartureAirport.Name == departureAirportName
                    && t.Type == "economy");

            var businessTicket = await context.Tickets
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

        public static async Task<IEnumerable<FlightDTO>> ToFlightDTOs(this IEnumerable<Flight> flights, AirportProjectDBContext context)
        {
            var flightDTOs = new List<FlightDTO>();

            foreach (var flight in flights)
            {
                var flightDTO = await flight.ToFlightDTO(context);

                flightDTOs.Add(flightDTO);
            }

            return flightDTOs;
        }
    }
}
