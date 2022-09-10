using AirportProject.Application.Passengers.Queries.GetPassengersWithPagination;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Abstract
{
    public interface IPassengerRepository
    {
        public Task<PassengerDTO> Create(PassengerDTO passengerDTO);
        public Task<ICollection<Passenger>> GetRange(
            GetPassengersWithPaginationQuery query, CancellationToken cancellationToken);
        public Task<bool> Update(PassengerDTO passengerDTO);
        public Task<bool> Delete(int id);

        public Task<PassengerDTO> SearchByPassport(string passport);
        public Task<ICollection<PassengerDTO>> SearchByFirstname(string firstname);
        public Task<ICollection<PassengerDTO>> SearchByLastname(string lastname);

        public Task<bool> AddTicket(int passengerId, int ticketId);
        public Task<bool> DeleteTicket(int passengerId, int ticketId);

        public Task<int> GetTotalCount(CancellationToken cancellationToken);
    }
}
