﻿using AirportProject.Application.Abstract;
using AirportProject.Application.Casting;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport
{
    public class GetFlightsByDepartureAirportQueryHandler :
        IRequestHandler<GetFlightsByDepartureAirportQuery, IEnumerable<FlightDTO>>
    {
        private readonly IFlightRepository repository;
        private readonly FlightsCaster caster;

        public GetFlightsByDepartureAirportQueryHandler(IFlightRepository repository, FlightsCaster caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<IEnumerable<FlightDTO>> Handle(GetFlightsByDepartureAirportQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Airport name must be not empty and not be greater than 50 characters long");
            }

            var flights = await this.repository.SearchByFlightDepartureAirport(request, cancellationToken);

            return await this.caster.Cast(flights, cancellationToken);
        }
    }
}
