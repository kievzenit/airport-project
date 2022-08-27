using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
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

        public GetFLightsByArrivalAirportQueryHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<FlightDTO>> Handle(
            GetFlightsByArrivalAirportQuery request,
            CancellationToken cancellationToken)
        {
            if (request.AirportName == null || request.AirportName.Length > 50)
            {
                throw new ArgumentException("Airport name must be not empty and not be greater than 50 characters long");
            }

            return await this.repository.SearchByFlightArrivalAirport(request.AirportName);
        }
    }
}
