using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
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
        private readonly ICaster<Passenger, PassengerDTO> caster;

        public GetPassengersWithPaginatonQueryHandler(
            IPassengerRepository repository, ICaster<Passenger, PassengerDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<PageResultDTO<PassengerDTO>> Handle(
            GetPassengersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Page must be not equal or less than zero");
            }

            var passengers = await this.repository.GetRange(request, cancellationToken);
            var totalCount = await this.repository.GetTotalCount(cancellationToken);

            var passengerDTOs = await this.caster.Cast(passengers, cancellationToken);

            return new PageResultDTO<PassengerDTO>(passengerDTOs, totalCount);
        }
    }
}
