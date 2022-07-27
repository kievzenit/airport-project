using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using AirportProject.Infrastructure.Persistent.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        const int PAGESIZE = 6;
        private readonly IAirportRepository repository;

        public AirportController(IAirportRepository repository)
        {
            this.repository = repository;
        }


        [HttpGet("all")]
        public async Task<IEnumerable<AirportDTO>> Index()
        {
            return await repository.GetAll();
        }

        [HttpGet("{page}")]
        public async Task<IEnumerable<AirportDTO>> Page(int page)
        {
            return await repository.GetRange(page, PAGESIZE);
        }

        [HttpPost]
        public async Task<AirportDTO> Create([FromBody] AirportDTO airportDTO)
        {
            if (!airportDTO.IsValid())
            {
                this.Response.StatusCode = 400;
                return default;
            }

            return await repository.Create(airportDTO);
        }

        [HttpPut]
        public async Task Update([FromBody] AirportDTO airportDTO)
        {
            if (airportDTO.Id <= 0 || !airportDTO.IsValid())
            {
                this.Response.StatusCode = 400;
                return;
            }

            var success = await repository.Update(airportDTO);

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

            var success = await repository.Delete(id);

            if (!success)
            {
                this.Response.StatusCode = 404;
                return;
            }
        }
    }
}
