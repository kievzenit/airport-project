using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Airports.Queries.GetAirportsWithPagination
{
    public class GetAirportsWithPaginationQueryHandler :
        IRequestHandler<GetAirportsWithPaginationQuery, PageResultDTO<AirportDTO>>
    {
        private readonly IAirportRepository repository;

        public GetAirportsWithPaginationQueryHandler(IAirportRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PageResultDTO<AirportDTO>> Handle(
            GetAirportsWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0)
            {
                throw new ArgumentException("Page number must be not equal or less than zero");
            }

            var airportDTOs = await this.repository.GetRange(request.PageNumber, request.PageSize);
            var totalCount = await this.repository.GetTotalCount();

            return new PageResultDTO<AirportDTO>(airportDTOs, totalCount);
        }
    }
}
