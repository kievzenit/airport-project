using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    public interface IAirportRepository
    {
        public Task<AirportDTO> Create(AirportDTO airportDTO);
        public Task<IEnumerable<AirportDTO>> GetAll();
        public Task<IEnumerable<AirportDTO>> GetRange(int offset, int count);
        public Task<bool> Update(AirportDTO airportDTO);
        public Task<bool> Delete(int id);
    }
}
