using AirportProject.Domain.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport
{
    public record GetFlightsByArrivalAirportQuery(string airportName) : IRequest<IEnumerable<FlightDTO>>
    {
        public string AirportName { get; init; } = airportName;
    }
}
