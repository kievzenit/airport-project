using AirportProject.Application.Common.DTOs;
using MediatR;

namespace AirportProject.Application.Flights.Queries.GetFlightsWithPagination
{
    public record GetFlightsWithPaginationQuery(int page) : IRequest<PageResultDTO<FlightDTO>>
    {
        public int PageNumber { get; init; } = page;
        public int PageSize { get; init; } = 6;
    }
}
