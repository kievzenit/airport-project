using AirportProject.Application.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport
{
    public record GetFlightsByDepartureAirportQuery(string airportName) : IRequest<IEnumerable<FlightDTO>>
    {
        public string AirportName { get; init; } = airportName;
    }
}
