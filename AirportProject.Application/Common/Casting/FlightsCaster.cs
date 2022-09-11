using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Common.Casting
{
    public class FlightsCaster
    {
        private readonly IFlightRepository repository;

        public FlightsCaster(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<FlightDTO> Cast(Flight flight, CancellationToken cancellationToken)
        {
            var tickets = await this.repository.GetTicketsByFlight(flight, cancellationToken);
            var economyTicket = tickets.Item1;
            var businessticket = tickets.Item2;

            var flightDTO = new FlightDTO
            {
                Id = flight.Id,
                ArrivalAirportName = flight.ArrivalAirport.Name,
                DepartureAirportName = flight.DepartureAirport.Name,
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                Status = flight.Status,
                Terminal = flight.Terminal,
                EconomyPrice = economyTicket.Price,
                BusinessPrice = businessticket.Price
            };

            return flightDTO;
        }

        public async Task<ICollection<FlightDTO>> Cast(
            ICollection<Flight> flights, CancellationToken cancellationToken)
        {
            var flightDTOs = new List<FlightDTO>();

            foreach (var flight in flights)
            {
                var flightDTO = await this.Cast(flight, cancellationToken);

                flightDTOs.Add(flightDTO);
            }

            return flightDTOs;
        }
    }
}
