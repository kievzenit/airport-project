using MediatR;

namespace AirportProject.Application.Airports.Command.DeleteAirport
{
    public record DeleteAirportCommand(int id) : IRequest
    {
        public int Id { get; init; } = id;
    }
}
