using AirportProject.Domain.DTOs;
using AirportProject.Infrastructure.Persistent.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository repository;

        public TicketsController(ITicketRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("passenger/{passengerId}")]
        public async Task<IEnumerable<TicketDTO>> GetTickets(int passengerId)
        {
            return await this.repository.GetTickets(passengerId);
        }

        [HttpPost("search")]
        public async Task<IEnumerable<TicketDTO>> Search(TicketDTO ticketDTO)
        {
            return await this.repository.GetTickets(ticketDTO);
        }
    }
}
