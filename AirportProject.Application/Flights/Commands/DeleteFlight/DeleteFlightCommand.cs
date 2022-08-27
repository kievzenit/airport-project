using MediatR;

namespace AirportProject.Application.Flights.Commands.DeleteFlight
{
    public record DeleteFlightCommand(int id) : IRequest
    {
        public int Id { get; init; } = id;
    }
}
