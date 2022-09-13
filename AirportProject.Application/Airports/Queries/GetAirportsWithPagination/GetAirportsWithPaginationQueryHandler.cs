using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
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
        private readonly ICaster<Airport, AirportDTO> caster;

        public GetAirportsWithPaginationQueryHandler(
            IAirportRepository repository, ICaster<Airport, AirportDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<PageResultDTO<AirportDTO>> Handle(
            GetAirportsWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0)
            {
                throw new ArgumentException("Page number must be not equal or less than zero");
            }

            var airports = await this.repository.GetRange(
                request.PageNumber, request.PageSize, cancellationToken);
            var totalCount = await this.repository.GetTotalCount(cancellationToken);

            var airportDTOs = await this.caster.Cast(airports, cancellationToken);

            return new PageResultDTO<AirportDTO>(airportDTOs, totalCount);
        }
    }
}
