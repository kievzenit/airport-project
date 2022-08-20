using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Application.Abstract
{
    public interface IAirportRepository
    {
        public Task<AirportDTO> Create(AirportDTO airportDTO);
        public Task<ICollection<AirportDTO>> GetAll();
        public Task<ICollection<AirportDTO>> GetRange(int offset, int count);
        public Task<bool> Update(AirportDTO airportDTO);
        public Task<bool> Delete(int id);

        public Task<int> GetTotalCount();
    }
}
