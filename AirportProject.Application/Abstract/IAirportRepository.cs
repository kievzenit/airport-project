using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Abstract
{
    public interface IAirportRepository
    {
        public Task<Airport> Create(
            CreateAirportCommand createAirportCommand, CancellationToken cancellationToken);
        public Task<ICollection<AirportDTO>> GetAll();
        public Task<ICollection<AirportDTO>> GetRange(int offset, int count);
        public Task<bool> Update(AirportDTO airportDTO);
        public Task<bool> Delete(int id);

        public Task<bool> DoesAirportExists(string name, CancellationToken cancellationToken);

        public Task<int> GetTotalCount();
    }
}
