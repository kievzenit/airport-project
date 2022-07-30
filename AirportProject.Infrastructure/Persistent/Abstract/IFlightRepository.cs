using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    public interface IFlightRepository
    {
        public Task<FlightDTO> Create(FlightDTO flightDTO);
        public Task<ICollection<FlightDTO>> GetAll();
        public Task<ICollection<FlightDTO>> GetRange(int offset, int count);
        public Task<bool> Update(FlightDTO flightDTO);
        public Task<bool> Delete(int id);

        public Task<FlightDTO> SearchByFlightNumber(int id);
        public Task<ICollection<FlightDTO>> SearchByFlightArrivalAirport(string airportName);
        public Task<ICollection<FlightDTO>> SearchByFlightDepartureAirport(string airportName);

        public Task<int> GetTotalCount();
    }
}
