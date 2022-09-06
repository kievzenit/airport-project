using AirportProject.Application.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Flights.Queries.GetFlightsWithPagination
{
    public class GetFlightsWithPaginationQueryHandler :
        IRequestHandler<GetFlightsWithPaginationQuery, PageResultDTO<FlightDTO>>
    {
        private readonly IFlightRepository repository;

        public GetFlightsWithPaginationQueryHandler(IFlightRepository repository)
        {
            this.repository = repository;
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

            var flightDTOs = await this.ConvertToFlightDTOs(flights, cancellationToken);

            return new PageResultDTO<FlightDTO>(flightDTOs, totalCount);
        }

        public async Task<ICollection<FlightDTO>> ConvertToFlightDTOs(
            ICollection<Flight> flights, CancellationToken cancellationToken)
        {
            var flightDTOs = new List<FlightDTO>(flights.Count);

            foreach (var flight in flights)
            {
                var tickets = await this.repository.GetTicketsByFlight(flight, cancellationToken);
                var economyTicket = tickets.Item1;
                var businessticket = tickets.Item2;

                var flightDTO = new FlightDTO
                {
                    Id = flight.Id,
                    ArrivalAirportName = flight.ArrivalAirport.Name,
                    DepartureAirportName = flight.DepartureAirport.Name,
                    ArrivalTime = flight.ArrivalTime,
                    DepartureTime = flight.DepartureTime,
                    Status = flight.Status,
                    Terminal = flight.Terminal,
                    EconomyPrice = economyTicket.Price,
                    BusinessPrice = businessticket.Price
                };

                flightDTOs.Add(flightDTO);
            }

            return flightDTOs;
        }
    }
}
