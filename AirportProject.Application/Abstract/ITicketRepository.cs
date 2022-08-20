using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Application.Abstract
{
    public interface ITicketRepository
    {
        public Task<ICollection<TicketDTO>> GetTickets(TicketDTO ticketDTO);
        public Task<ICollection<TicketDTO>> GetTickets(int passengerId);
    }
}
