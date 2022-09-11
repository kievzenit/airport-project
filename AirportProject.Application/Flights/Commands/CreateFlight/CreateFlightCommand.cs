using AirportProject.Application.Common.DTOs;
using MediatR;
using System;

namespace AirportProject.Application.Flights.Commands.CreateFlight
{
    public record CreateFlightCommand : IRequest<FlightDTO>
    {
        public string ArrivalAirportName { get; init; }
        public string DepartureAirportName { get; init; }
        public char Terminal { get; init; }
        public DateTime ArrivalTime { get; init; }
        public DateTime DepartureTime { get;init; }
        public string Status { get; init; } 
        public decimal EconomyPrice { get; init; }
        public decimal BusinessPrice { get; init; }
    }
}
