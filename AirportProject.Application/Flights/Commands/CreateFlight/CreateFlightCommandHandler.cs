using System;
using System.Threading;
using System.Threading.Tasks;
using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using MediatR;

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
            var flightDTO = new FlightDTO
            {
                ArrivalAirportName = request.ArrivalAirportName,
                DepartureAirportName = request.DepartureAirportName,
                Terminal = request.Terminal,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Status = request.Status,
                EconomyPrice = request.EconomyPrice,
                BusinessPrice = request.BusinessPrice
            };

            if (!flightDTO.IsValid())
            {
                throw new ArgumentException("input data was in incorrect format");
            }

            return await this.repository.Create(flightDTO);
        }
    }
}
