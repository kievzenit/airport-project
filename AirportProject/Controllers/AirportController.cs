﻿using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Application.Airports.Commands.DeleteAirport;
using AirportProject.Application.Airports.Commands.UpdateAirport;
using AirportProject.Application.Airports.Queries.GetAirportsWithPagination;
using AirportProject.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    public class AirportController : BaseApiController
    {
        [HttpGet("{page}")]
        public async Task<IActionResult> Page(int page, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetAirportsWithPaginationQuery(page), cancellationToken);

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
            [FromBody] CreateAirportCommand createAirportCommand, 
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(createAirportCommand, cancellationToken);

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
            [FromBody] UpdateAirportCommand updateAirportCommand,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.Mediator.Send(updateAirportCommand, cancellationToken);

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
                await this.Mediator.Send(new DeleteAirportCommand(id), cancellationToken);

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
    }
}
