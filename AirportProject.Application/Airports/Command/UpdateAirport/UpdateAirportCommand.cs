using AirportProject.Domain.DTOs;
using MediatR;

namespace AirportProject.Application.Airports.Commands.UpdateAirport
{
    public record UpdateAirportCommand : IRequest
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
    }
}
