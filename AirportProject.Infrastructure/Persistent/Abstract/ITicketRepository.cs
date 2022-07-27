using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    public interface ITicketRepository
    {
        public Task<IEnumerable<TicketDTO>> GetTickets(TicketDTO ticketDTO);
        public Task<IEnumerable<TicketDTO>> GetTickets(int passengerId);
    }
}
