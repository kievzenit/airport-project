using AirportProject.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Abstract
{
    public interface IPassengerRepository
    {
        public Task<PassengerDTO> Create(PassengerDTO passengerDTO);
        public Task<ICollection<PassengerDTO>> GetAll();
        public Task<ICollection<PassengerDTO>> GetRange(int offset, int count);
        public Task<bool> Update(PassengerDTO passengerDTO);
        public Task<bool> Delete(int id);

        public Task<PassengerDTO> SearchByPassport(string passport);
        public Task<ICollection<PassengerDTO>> SearchByFirstname(string firstname);
        public Task<ICollection<PassengerDTO>> SearchByLastname(string lastname);

        public Task<bool> AddTicket(int passengerId, int ticketId);
        public Task<bool> DeleteTicket(int passengerId, int ticketId);

        public Task<int> GetTotalCount();
    }
}
