﻿using AirportProject.Application.Common.Exceptions;

using AirportProject.Application.Flights.Commands.CreateFlight;
using AirportProject.Application.Flights.Commands.DeleteFlight;
using AirportProject.Application.Flights.Commands.UpdateFlight;

using AirportProject.Application.Flights.Queries.GetFlightById;
using AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport;
using AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport;
using AirportProject.Application.Flights.Queries.GetFlightsWithPagination;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    public class FlightController : BaseApiController
    {
        [HttpGet("{page}")]
        public async Task<IActionResult> GetPage(int page, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetFlightsWithPaginationQuery(page), cancellationToken);

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
            [FromBody] CreateFlightCommand createFlightCommand, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(createFlightCommand, cancellationToken);

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
            [FromBody] UpdateFlightCommand updateFlightCommand, CancellationToken cancellationToken)
        {
            try
            {
                await this.Mediator.Send(updateFlightCommand, cancellationToken);

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
                await this.Mediator.Send(new DeleteFlightCommand(id), cancellationToken);

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

        [HttpGet("search/byId/{flightId}")]
        public async Task<IActionResult> SearchByFlightId(int flightId, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(new GetFlightByIdQuery(flightId), cancellationToken);

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

        [HttpGet("search/byArrivalAirport/{airportName}")]
        public async Task<IActionResult> SearchByFlightArrivalAirport(
            string airportName, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetFlightsByArrivalAirportQuery(airportName),
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

        [HttpGet("search/byDepartureAirport/{airportName}")]
        public async Task<IActionResult> SearchByFlightDepartureAirport(
            string airportName, CancellationToken cancellationToken)
        {
            try
            {
                var response = await this.Mediator.Send(
                    new GetFlightsByDepartureAirportQuery(airportName),
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
    }
}
