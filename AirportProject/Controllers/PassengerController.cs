using AirportProject.Domain.DTOs;
using AirportProject.Domain.DTOs.Validation;
using AirportProject.Infrastructure.Persistent.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirportProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        const int PAGESIZE = 6;
        private readonly IPassengerRepository repository;

        public PassengerController(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<PassengerDTO>> GetAll()
        {
            return await this.repository.GetAll();
        }

        [HttpGet("{page}")]
        public async Task<IEnumerable<PassengerDTO>> GetPage(int page)
        {
            return await this.repository.GetRange(page, PAGESIZE);
        }

        [HttpPost]
        public async Task<PassengerDTO> Create([FromBody] PassengerDTO passengerDTO)
        {
            if (!passengerDTO.IsValid())
            {
                this.Response.StatusCode = 400;
                return default;
            }

            return await this.repository.Create(passengerDTO);
        }

        [HttpPut]
        public async Task Update([FromBody] PassengerDTO passengerDTO)
        {
            if (passengerDTO.Id <= 0 || !passengerDTO.IsValid(false))
            {
                this.Response.StatusCode = 400;
                return;
            }

            var success = await this.repository.Update(passengerDTO);

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

        [HttpPut("ticket")]
        public async Task AddTicket(int passengerId, int ticketId)
        {
            if (ticketId <= 0 && passengerId <= 0)
            {
                this.Response.StatusCode = 400;
                return;
            }

            var success = await this.repository.AddTicket(passengerId, ticketId);

            if (!success)
            {
                this.Response.StatusCode = 404;
                return;
            }
        }

        [HttpDelete("ticket")]
        public async Task DeleteTicket(int passengerId, int ticketId)
        {
            if (ticketId <= 0 && passengerId <= 0)
            {
                this.Response.StatusCode = 400;
                return;
            }

            var success = await this.repository.DeleteTicket(passengerId, ticketId);

            if (!success)
            {
                this.Response.StatusCode = 404;
                return;
            }
        }

        [HttpGet("search/byPassport/{passport}")]
        public async Task<PassengerDTO> SearchByPassport(string passport)
        {
            if (passport == null || passport.Length != 8 || !Regex.IsMatch(passport, "^[a-z]{2}\\d{6}$"))
            {
                this.Response.StatusCode = 400;
                return default;
            }

            var passengerDTO = await this.repository.SearchByPassport(passport);

            if (passengerDTO == null)
            {
                this.Response.StatusCode = 404;
                return default;
            }

            return passengerDTO;
        }

        [HttpGet("search/byFirtsname/{firstname}")]
        public async Task<IEnumerable<PassengerDTO>> SearchByFirstname(string firstname)
        {
            if (firstname == null || firstname.Length > 50 || firstname.Length == 0)
            {
                this.Response.StatusCode = 400;
                return default;
            }

            return await this.repository.SearchByFirstname(firstname);
        }

        [HttpGet("search/byLastname/{lastname}")]
        public async Task<IEnumerable<PassengerDTO>> SearchByLastname(string lastname)
        {
            if (lastname == null || lastname.Length > 50 || lastname.Length == 0)
            {
                this.Response.StatusCode = 400;
                return default;
            }

            return await this.repository.SearchByLastname(lastname);
        }
    }
}
