using AirportProject.Application.Tickets.Queries.GetSpecificTickets;
using AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Abstract
{
    public interface ITicketRepository
    {
        public Task<ICollection<Ticket>> GetTickets(
            GetSpecificTicketsQuery query, CancellationToken cancellationToken);
        public Task<ICollection<Ticket>> GetTickets(
            GetTicketsByPassengerIdQuery query, CancellationToken cancellationToken);
    }
}
