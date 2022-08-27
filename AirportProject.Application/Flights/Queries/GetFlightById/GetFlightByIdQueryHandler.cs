using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightById
{
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDTO>
    {
        private readonly IFlightRepository repository;

        public GetFlightByIdQueryHandler(IFlightRepository repository)
        {
            this.repository = repository;
        }

        public async Task<FlightDTO> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new ArgumentException("Flight id must be not eqaul or less than zero");
            }

            var flightDTO = await this.repository.SearchByFlightNumber(request.Id);

            if (flightDTO == null)
            {
                throw new NotFoundException($"Flight with id: {request.Id} not found");
            }

            return flightDTO;
        }
    }
}
