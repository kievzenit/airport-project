using AirportProject.Domain.DTOs;
using MediatR;

namespace AirportProject.Application.Airports.Queries.GetAirportsWithPagination
{
    public record GetAirportsWithPaginationQuery(int pageNumber) : IRequest<PageResultDTO<AirportDTO>>
    {
        public int PageNumber { get; init; } = pageNumber;
        public int PageSize { get; init; } = 6;
    }
}
