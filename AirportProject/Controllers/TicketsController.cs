using AirportProject.Application.Tickets.Queries.GetSpecificTickets;
using AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    public class TicketsController : BaseApiController
    {
        [HttpGet("passenger/{passengerId}")]
        public async Task<IActionResult> GetTickets(int passengerId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetTicketsByPassengerIdQuery(passengerId),
                    cancellationToken);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(GetSpecificTicketsQuery getSpecificTicketsQuery)
        {
            try
            {
                var response = await this.Mediator.Send(getSpecificTicketsQuery);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
