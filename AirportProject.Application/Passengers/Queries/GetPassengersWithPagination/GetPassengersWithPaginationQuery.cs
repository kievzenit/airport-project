using AirportProject.Application.Common.DTOs;
using MediatR;

namespace AirportProject.Application.Passengers.Queries.GetPassengersWithPagination
{
    public record GetPassengersWithPaginationQuery(int page) : IRequest<PageResultDTO<PassengerDTO>>
    {
        public int PageNumber { get; init; } = page;
        public int PageSize { get; init; } = 6;
    }
}
