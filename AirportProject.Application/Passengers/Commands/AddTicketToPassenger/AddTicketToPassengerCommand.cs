using MediatR;

namespace AirportProject.Application.Passengers.Commands.AddTicketToPassenger
{
    public record AddTicketToPassengerCommand(int passengerId, int ticketId) : IRequest
    {
        public int PassengerId { get; init; } = passengerId;
        public int TicketId { get; init; } = ticketId;
    }
}
