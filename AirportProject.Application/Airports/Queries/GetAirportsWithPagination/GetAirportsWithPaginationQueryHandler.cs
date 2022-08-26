using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
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
            var airportDTOs = await this.repository.GetRange(request.PageNumber, request.PageSize);
            var totalCount = await this.repository.GetTotalCount();

            return new PageResultDTO<AirportDTO>(airportDTOs, totalCount);
        }
    }
}
