using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Casting;
using AirportProject.Application.Exceptions;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightById
{
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDTO>
    {
        private readonly IFlightRepository repository;
        private readonly FlightsCaster caster;

        public GetFlightByIdQueryHandler(IFlightRepository repository, FlightsCaster caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<FlightDTO> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Flight id must be not eqaul or less than zero");
            }

            var flight = await this.repository.SearchByFlightNumber(request, cancellationToken);

            if (flight == null)
            {
                throw new NotFoundException($"Flight with id: {request.Id} not found");
            }

            var flightDTO = await this.caster.Cast(flight, cancellationToken);

            return flightDTO;
        }
    }
}
