using AirportProject.Domain;
using AirportProject.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Casting
{
    public static class AirportCasting
    {
        public static Task<Airport> ToAirport(this AirportDTO airportDTO)
        {
            return Task.FromResult(
                new Airport
                {
                    Id = airportDTO.Id,
                    Name = airportDTO.Name,
                    Country = airportDTO.Country,
                    City = airportDTO.City
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
