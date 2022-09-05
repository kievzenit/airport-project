using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Casting
{
    public static class AirportCasting
    {
        public static Task<Airport> ToAirport(this CreateAirportCommand createAirportCommand)
        {
            return Task.FromResult(
                new Airport
                {
                    Name = createAirportCommand.Name,
                    Country = createAirportCommand.Country,
                    City = createAirportCommand.City
                });
        }

        public static Task<AirportDTO> ToAirportDTO(this Airport airport)
        {
            return Task.FromResult(
                new AirportDTO
                {
                    Id = airport.Id,
                    Name = airport.Name,
                    Country = airport.Country,
                    City = airport.City
                });
        }

        public static async Task<List<AirportDTO>> ToAirportDTOs(this IEnumerable<Airport> airports)
        {
            var airportDTOs = new List<AirportDTO>();

            foreach (var airport in airports)
            {
                var airportDTO = await airport.ToAirportDTO();

                airportDTOs.Add(airportDTO);
            }

            return airportDTOs;
        }
    }
}
