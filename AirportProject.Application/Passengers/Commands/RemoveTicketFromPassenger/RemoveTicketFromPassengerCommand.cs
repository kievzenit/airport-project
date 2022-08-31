using MediatR;

namespace AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger
{
    public record RemoveTicketFromPassengerCommand(int passengerId, int ticketId) : IRequest
    {
        public int PassengerId { get; init; } = passengerId;
        public int TicketId { get; init; } = ticketId;
    }
}
