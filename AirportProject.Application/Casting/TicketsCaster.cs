using AirportProject.Application.Common.Abstract;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Application.Casting
{
    public class TicketsCaster
    {
        private readonly ITicketRepository repository;

        public TicketsCaster(ITicketRepository repository)
        {
            this.repository = repository;
        }

        public Task<TicketDTO> Cast(Ticket ticket)
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

        public async Task<ICollection<TicketDTO>> Cast(ICollection<Ticket> tickets)
        {
            var ticketDTOs = new List<TicketDTO>(tickets.Count);

            foreach (var ticket in tickets)
            {
                var ticketDTO = await this.Cast(ticket);

                ticketDTOs.Add(ticketDTO);
            }

            return ticketDTOs;
        }
    }
}
