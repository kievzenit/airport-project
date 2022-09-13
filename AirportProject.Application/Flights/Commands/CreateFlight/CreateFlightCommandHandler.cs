using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
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
        private readonly ICaster<Flight, FlightDTO> caster;

        public CreateFlightCommandHandler(IFlightRepository repository, ICaster<Flight, FlightDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<FlightDTO> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("input data was in incorrect format");
            }

            var flight = await this.repository.Create(request, cancellationToken);

            return await this.caster.Cast(flight, cancellationToken);
        }
    }
}
