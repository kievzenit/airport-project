using AirportProject.Application.Flights.Commands.CreateFlight;
using AirportProject.Application.Flights.Commands.DeleteFlight;
using AirportProject.Application.Flights.Commands.UpdateFlight;
using AirportProject.Application.Flights.Queries.GetFlightById;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Abstract
{
    public interface IFlightRepository
    {
        public Task<Flight> Create(CreateFlightCommand command, CancellationToken cancellationToken);
        public Task<ICollection<Flight>> GetRange(
            int offset, int count, CancellationToken cancellationToken);
        public Task<bool> Update(UpdateFlightCommand command, CancellationToken cancellationToken);
        public Task<bool> Delete(DeleteFlightCommand command, CancellationToken cancellationToken);

        public Task<Flight> SearchByFlightNumber(
            GetFlightByIdQuery query, CancellationToken cancellationToken);
        public Task<ICollection<FlightDTO>> SearchByFlightArrivalAirport(string airportName);
        public Task<ICollection<FlightDTO>> SearchByFlightDepartureAirport(string airportName);

        public Task<Tuple<Ticket, Ticket>> GetTicketsByFlight(
            Flight flight, CancellationToken cancellationToken);

        public Task<int> GetTotalCount(CancellationToken cancellationToken);
    }
}
