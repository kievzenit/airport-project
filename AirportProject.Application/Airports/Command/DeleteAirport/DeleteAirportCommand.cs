using MediatR;

namespace AirportProject.Application.Airports.Commands.DeleteAirport
{
    public record DeleteAirportCommand(int id) : IRequest
    {
        public int Id { get; init; } = id;
    }
}
