using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport
{
    public class GetFlightsByDepartureAirportQueryHandler :
        IRequestHandler<GetFlightsByDepartureAirportQuery, IEnumerable<FlightDTO>>
    {
        private readonly IFlightRepository repository;

        public GetFlightsByDepartureAirportQueryHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<FlightDTO>> Handle(GetFlightsByDepartureAirportQuery request, CancellationToken cancellationToken)
        {
            if (request.AirportName == null || request.AirportName.Length > 50)
            {
                throw new ArgumentException("Airport name must be not empty and not be greater than 50 characters long");
            }

            return await this.repository.SearchByFlightDepartureAirport(request.AirportName);
        }
    }
}
