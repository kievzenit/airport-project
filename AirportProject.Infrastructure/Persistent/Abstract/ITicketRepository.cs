using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    public interface ITicketRepository
    {
        public Task<ICollection<TicketDTO>> GetTickets(TicketDTO ticketDTO);
        public Task<ICollection<TicketDTO>> GetTickets(int passengerId);
    }
}
