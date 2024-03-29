﻿using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport
{
    public class GetFLightsByArrivalAirportQueryHandler :
        IRequestHandler<GetFlightsByArrivalAirportQuery, IEnumerable<FlightDTO>>
    {
        private readonly IFlightRepository repository;
        private readonly ICaster<Flight, FlightDTO> caster;

        public GetFLightsByArrivalAirportQueryHandler(
            IFlightRepository repository, ICaster<Flight, FlightDTO> caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<IEnumerable<FlightDTO>> Handle(
            GetFlightsByArrivalAirportQuery request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Airport name must be not empty and not be greater than 50 characters long");
            }

            var flights = await this.repository.SearchByFlightArrivalAirport(request, cancellationToken);

            return await this.caster.Cast(flights, cancellationToken);
        }
    }
}
