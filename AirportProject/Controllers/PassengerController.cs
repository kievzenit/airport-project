using AirportProject.Application.Exceptions;

using AirportProject.Application.Passengers.Queries.GetPassengersWithPagination;
using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using AirportProject.Application.Passengers.Queries.GetPassengersByFirstname;
using AirportProject.Application.Passengers.Queries.GetPassengersByLastname;

using AirportProject.Application.Passengers.Commands.AddTicketToPassenger;
using AirportProject.Application.Passengers.Commands.CreatePassenger;
using AirportProject.Application.Passengers.Commands.DeletePassenger;
using AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger;
using AirportProject.Application.Passengers.Commands.UpdatePassenger;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    public class PassengerController : BaseApiController
    {
        [HttpGet("{page}")]
        public async Task<IActionResult> GetPage(int page, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetPassengersWithPaginationQuery(page), cancellationToken);

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

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePassengerCommand createPassengerCommand, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(createPassengerCommand, cancellationToken);

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

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdatePassengerCommand updatePassengerCommand, CancellationToken cancellationToken)
        {
            try
            {
                await this.Mediator.Send(updatePassengerCommand, cancellationToken);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id, CancellationToken cancellationToken)
        {
            try
            {
                await this.Mediator.Send(new DeletePassengerCommand(id), cancellationToken);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("ticket")]
        public async Task<IActionResult> AddTicket(
            int passengerId, int ticketId, CancellationToken cancellationToken)
        {
            try
            {
                await this.Mediator.Send(
                    new AddTicketToPassengerCommand(passengerId, ticketId), cancellationToken);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("ticket")]
        public async Task<IActionResult> DeleteTicket(
            int passengerId, int ticketId, CancellationToken cancellationToken)
        {
            try
            {
                await this.Mediator.Send(
                    new RemoveTicketFromPassengerCommand(passengerId, ticketId),
                    cancellationToken);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("search/byPassport/{passport}")]
        public async Task<IActionResult> SearchByPassport(
            string passport, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetPassengerByPassportQuery(passport),
                    cancellationToken);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("search/byFirtsname/{firstname}")]
        public async Task<IActionResult> SearchByFirstname(string firstname)
        {
            try
            {
                var response = await this.Mediator.Send(new GetPassengersByFirstnameQuery(firstname));

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

        [HttpGet("search/byLastname/{lastname}")]
        public async Task<IActionResult> SearchByLastname(string lastname)
        {
            try
            {
                var response = await this.Mediator.Send(new GetPassengersByLastnameQuery(lastname));

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
