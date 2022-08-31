using MediatR;

namespace AirportProject.Application.Passengers.Commands.UpdatePassenger
{
    public record UpdatePassengerCommand : IRequest
    {
        public int Id { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string Passport { get; init; }
        public string Nationality { get; init; }
    }
}
