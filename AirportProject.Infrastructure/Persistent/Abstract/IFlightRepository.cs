using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    public interface IFlightRepository
    {
        public Task<FlightDTO> Create(FlightDTO flightDTO);
        public Task<IEnumerable<FlightDTO>> GetAll();
        public Task<IEnumerable<FlightDTO>> GetRange(int offset, int count);
        public Task<bool> Update(FlightDTO flightDTO);
        public Task<bool> Delete(int id);

        public Task<FlightDTO> SearchByFlightNumber(int id);
        public Task<IEnumerable<FlightDTO>> SearchByFlightArrivalAirport(string airportName);
        public Task<IEnumerable<FlightDTO>> SearchByFlightDepartureAirport(string airportName);
    }
}
