using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Queries.GetPassengersWithPagination
{
    public class GetPassengersWithPaginatonQueryHandler :
        IRequestHandler<GetPassengersWithPaginationQuery, PageResultDTO<PassengerDTO>>
    {
        private readonly IPassengerRepository repository;

        public GetPassengersWithPaginatonQueryHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PageResultDTO<PassengerDTO>> Handle(
            GetPassengersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0)
            {
                throw new ArgumentException("Page must be not equal or less than zero");
            }

            var passengerDTOs = await this.repository.GetRange(request.PageNumber, request.PageSize);
            var totalCount = await this.repository.GetTotalCount();

            return new PageResultDTO<PassengerDTO>(passengerDTOs, totalCount);
        }
    }
}
