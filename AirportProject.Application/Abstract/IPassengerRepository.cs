using AirportProject.Application.Passengers.Commands.AddTicketToPassenger;
using AirportProject.Application.Passengers.Commands.CreatePassenger;
using AirportProject.Application.Passengers.Commands.DeletePassenger;
using AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger;
using AirportProject.Application.Passengers.Commands.UpdatePassenger;
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
        public Task<Passenger> Create(CreatePassengerCommand command, CancellationToken cancellationToken);
        public Task<ICollection<Passenger>> GetRange(
            GetPassengersWithPaginationQuery query, CancellationToken cancellationToken);
        public Task<bool> Update(UpdatePassengerCommand command, CancellationToken cancellationToken);
        public Task<bool> Delete(DeletePassengerCommand command, CancellationToken cancellationToken);

        public Task<PassengerDTO> SearchByPassport(string passport);
        public Task<ICollection<PassengerDTO>> SearchByFirstname(string firstname);
        public Task<ICollection<PassengerDTO>> SearchByLastname(string lastname);

        public Task<bool> AddTicket(
            AddTicketToPassengerCommand command, CancellationToken cancellationToken);
        public Task<bool> DeleteTicket(
            RemoveTicketFromPassengerCommand command, CancellationToken cancellationToken);

        public Task<int> GetTotalCount(CancellationToken cancellationToken);
    }
}
