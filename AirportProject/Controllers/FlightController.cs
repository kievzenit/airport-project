using AirportProject.DTOs;
using AirportProject.DTOs.Validation;
using AirportProject.Infrastructure.Persistent.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        const int PAGESIZE = 6;
        private readonly IFlightRepository repository;

        public FlightController(IFlightRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<FlightDTO>> GetAll()
        {
            return await this.repository.GetAll();
        }

        [HttpGet("{page}")]
        public async Task<IEnumerable<FlightDTO>> GetPage(int page)
        {
            return await this.repository.GetRange(page, PAGESIZE);
        }

        [HttpPost]
        public async Task<FlightDTO> Create([FromBody] FlightDTO flightDTO)
        {
            if (!flightDTO.IsValid())
            {
                this.Response.StatusCode = 400;
                return default;
            }

            return await this.repository.Create(flightDTO);
        }

        [HttpPut]
        public async Task Update([FromBody] FlightDTO flightDTO)
        {
            if (flightDTO.Id <= 0 || !flightDTO.IsValid())
            {
                this.Response.StatusCode = 400;
                return;
            }

            var success = await this.repository.Update(flightDTO);

            if (!success)
            {
                this.Response.StatusCode = 404;
                return;
            }
        }

        [HttpDelete]
        public async Task Delete([FromBody] int id)
        {
            if (id <= 0)
            {
                this.Response.StatusCode = 400;
                return;
            }

            var success = await this.repository.Delete(id);

            if (!success)
            {
                this.Response.StatusCode = 404;
                return;
            }
        }
    }
}
