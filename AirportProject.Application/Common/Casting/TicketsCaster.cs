using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Common.Casting
{
    public class TicketsCaster : ICaster<Ticket, TicketDTO>
    {
        public Task<TicketDTO> Cast(Ticket ticket, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TicketDTO
            {
                From = ticket.Flight.DepartureAirport.Name,
                To = ticket.Flight.ArrivalAirport.Name,
                Type = ticket.Type,
                Id = ticket.Id,
                FlightId = ticket.FlightId,
                ArrivalTime = ticket.Flight.ArrivalTime,
                DepartureTime = ticket.Flight.DepartureTime,
                Price = ticket.Price
            });
        }

        public async Task<ICollection<TicketDTO>> Cast(
            ICollection<Ticket> tickets, CancellationToken cancellationToken)
        {
            var ticketDTOs = new List<TicketDTO>(tickets.Count);

            foreach (var ticket in tickets)
            {
                var ticketDTO = await this.Cast(ticket, cancellationToken);

                ticketDTOs.Add(ticketDTO);
            }

            return ticketDTOs;
        }
    }
}
