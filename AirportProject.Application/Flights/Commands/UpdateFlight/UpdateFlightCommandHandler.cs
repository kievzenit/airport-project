using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Commands.UpdateFlight
{
    public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand>
    {
        private readonly IFlightRepository repository;

        public UpdateFlightCommandHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            var flightDTO = new FlightDTO
            {
                Id = request.Id,
                ArrivalAirportName = request.ArrivalAirportName,
                DepartureAirportName = request.DepartureAirportName,
                Terminal = request.Terminal,
                ArrivalTime = request.ArrivalTime,
                DepartureTime = request.DepartureTime,
                Status = request.Status,
                EconomyPrice = request.EconomyPrice,
                BusinessPrice = request.BusinessPrice
            };

            if (flightDTO.Id <= 0 || !flightDTO.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var success = await this.repository.Update(flightDTO);

            if (!success)
            {
                throw new NotFoundException($"Flight with id: {request.Id} was not found");
            }

            return Unit.Value;
        }
    }
}
