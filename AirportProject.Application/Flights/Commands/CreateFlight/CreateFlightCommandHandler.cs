using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Commands.CreateFlight
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightDTO>
    {
        private readonly IFlightRepository repository;

        public CreateFlightCommandHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<FlightDTO> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("input data was in incorrect format");
            }

            var flight = await this.repository.Create(request, cancellationToken);

            return await this.ConvertToFlightDTO(flight, cancellationToken);
        }

        private async Task<FlightDTO> ConvertToFlightDTO(Flight flight, CancellationToken cancellationToken)
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
    }
}
