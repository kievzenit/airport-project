using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Common.Casting
{
    public class AirportsCaster : ICaster<Airport, AirportDTO>
    {
        public Task<AirportDTO> Cast(Airport airport, CancellationToken cancellationToken)
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

        public async Task<ICollection<AirportDTO>> Cast(
            ICollection<Airport> airports, CancellationToken cancellationToken)
        {
            var airportDTOs = new List<AirportDTO>();

            foreach (var airport in airports)
            {
                var airportDTO = await this.Cast(airport, cancellationToken);

                airportDTOs.Add(airportDTO);
            }

            return airportDTOs;
        }
    }
}
