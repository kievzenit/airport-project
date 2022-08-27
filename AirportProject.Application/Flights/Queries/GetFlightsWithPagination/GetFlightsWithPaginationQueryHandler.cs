using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightsWithPagination
{
    public class GetFlightsWithPaginationQueryHandler :
        IRequestHandler<GetFlightsWithPaginationQuery, PageResultDTO<FlightDTO>>
    {
        private readonly IFlightRepository repository;

        public GetFlightsWithPaginationQueryHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PageResultDTO<FlightDTO>> Handle(GetFlightsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0)
            {
                throw new ArgumentException("Page must be not equal or less than zero");
            }

            var flightDTOs = await this.repository.GetRange(request.PageNumber, request.PageSize);
            var totalCount = await this.repository.GetTotalCount();

            return new PageResultDTO<FlightDTO>(flightDTOs, totalCount);
        }
    }
}
