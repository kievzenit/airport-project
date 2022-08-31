using MediatR;

namespace AirportProject.Application.Passengers.Commands.DeletePassenger
{
    public record DeletePassengerCommand(int id) : IRequest
    {
        public int Id { get; init; } = id;
    }
}
