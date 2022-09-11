using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
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

            var airports = await this.repository.GetRange(
                request.PageNumber, request.PageSize, cancellationToken);
            var totalCount = await this.repository.GetTotalCount(cancellationToken);

            var airportDTOs = new List<AirportDTO>();

            foreach (var airport in airports)
            {
                var airportDTO = new AirportDTO
                {
                    Id = airport.Id,
                    Name = airport.Name,
                    Country = airport.Country,
                    City = airport.City
                };

                airportDTOs.Add(airportDTO);
            }

            return new PageResultDTO<AirportDTO>(airportDTOs, totalCount);
        }
    }
}
