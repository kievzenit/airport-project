using AirportProject.Domain.DTOs;
using MediatR;

namespace AirportProject.Application.Airports.Commands.CreateAirport
{
    public record CreateAirportCommand : IRequest<AirportDTO>
    {
        public string Name { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
    }
}
