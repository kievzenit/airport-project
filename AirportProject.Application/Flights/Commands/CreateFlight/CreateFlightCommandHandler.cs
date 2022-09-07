using AirportProject.Application.Abstract;
using AirportProject.Application.Casting;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Commands.CreateFlight
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightDTO>
    {
        private readonly IFlightRepository repository;
        private readonly FlightsCaster caster;

        public CreateFlightCommandHandler(IFlightRepository repository, FlightsCaster caster)
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
