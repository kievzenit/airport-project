﻿using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
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
        private readonly ICaster<Flight, FlightDTO> caster;

        public GetFlightsWithPaginationQueryHandler(
            IFlightRepository repository, ICaster<Flight, FlightDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<PageResultDTO<FlightDTO>> Handle(
            GetFlightsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Page must be not equal or less than zero");
            }

            var flights = await this.repository.GetRange(
                request.PageNumber, request.PageSize, cancellationToken);
            var totalCount = await this.repository.GetTotalCount(cancellationToken);

            var flightDTOs = await this.caster.Cast(flights, cancellationToken);

            return new PageResultDTO<FlightDTO>(flightDTOs, totalCount);
        }
    }
}
