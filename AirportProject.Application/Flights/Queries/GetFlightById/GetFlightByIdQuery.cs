using AirportProject.Application.Common.DTOs;
using MediatR;

namespace AirportProject.Application.Flights.Queries.GetFlightById
{
    public record GetFlightByIdQuery(int id) : IRequest<FlightDTO>
    {
        public int Id { get; init; } = id;
    }
}
