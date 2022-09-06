using AirportProject.Application.Airports.Commands.CreateAirport;
using AirportProject.Application.Airports.Commands.UpdateAirport;
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
        public Task<ICollection<Airport>> GetRange(
            int offset, int count, CancellationToken cancellationToken);
        public Task<bool> Update(
            UpdateAirportCommand updateAirportCommand, CancellationToken cancellationToken);
        public Task<bool> Delete(int id, CancellationToken cancellationToken);

        public Task<bool> DoesAirportExists(string name, CancellationToken cancellationToken);

        public Task<int> GetTotalCount(CancellationToken cancellationToken);
    }
}
