using AirportProject.Domain.DTOs;
using MediatR;
using System;

namespace AirportProject.Application.Flights.Commands.UpdateFlight
{
    public record UpdateFlightCommand : IRequest
    {
        public int Id { get; init; }
        public string ArrivalAirportName { get; init; }
        public char Terminal { get; init; }
        public DateTime ArrivalTime { get; init; }
        public DateTime DepartureTime { get; init; }
        public string Status { get; init; }
        public decimal EconomyPrice { get; init; }
        public decimal BusinessPrice { get; init; }
    }
}
