using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Casting
{
    public static class TicketCasting
    {
        public static Task<TicketDTO> ToTicketDTO(this Ticket ticket)
        {
            return Task.FromResult(
                new TicketDTO
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

        public static async Task<IEnumerable<TicketDTO>> ToTicktDTOs(this IEnumerable<Ticket> tickets)
        {
            var ticketDTOs = new List<TicketDTO>();

            foreach (var ticket in tickets)
            {
                var ticketDTO = await ticket.ToTicketDTO();

                ticketDTOs.Add(ticketDTO);
            }

            return ticketDTOs;
        }
    }
}
