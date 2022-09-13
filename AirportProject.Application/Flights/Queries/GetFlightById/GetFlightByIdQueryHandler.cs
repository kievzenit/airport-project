using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightById
{
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDTO>
    {
        private readonly IFlightRepository repository;
        private readonly ICaster<Flight, FlightDTO> caster;

        public GetFlightByIdQueryHandler(IFlightRepository repository, ICaster<Flight, FlightDTO> caster)
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
