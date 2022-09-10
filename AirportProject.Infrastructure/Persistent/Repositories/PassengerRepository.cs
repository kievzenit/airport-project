using AirportProject.Application.Abstract;
using AirportProject.Application.Passengers.Commands.CreatePassenger;
using AirportProject.Application.Passengers.Commands.UpdatePassenger;
using AirportProject.Application.Passengers.Queries.GetPassengersWithPagination;
using AirportProject.Domain.DTOs;
using AirportProject.Domain.Models;
using AirportProject.Infrastructure.Persistent.Casting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly AirportProjectDBContext context;

        public PassengerRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<Passenger> Create(
            CreatePassengerCommand command, CancellationToken cancellationToken)
        {
            var passenger = new Passenger
            {
                Firstname = command.Firstname,
                Lastname = command.Lastname,
                Passport = command.Passport,
                Nationality = command.Nationality,
                Birthday = command.Birthday,
                Gender = command.Gender
            };

            await this.context.AddAsync(passenger, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return passenger;
        }

        public async Task<bool> Delete(int id)
        {
            var passenger = await this.context.Passengers.FirstOrDefaultAsync(p => p.Id == id);

            if (passenger == null)
                return false;

            var relatedPassengersTickets = await this.context.PassengersTickets
                .Where(pt => pt.Passenger == passenger)
                .ToListAsync();

            this.context.RemoveRange(relatedPassengersTickets);
            this.context.Remove(passenger);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<Passenger>> GetRange(
            GetPassengersWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var passengers = await this.context.Passengers
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            return passengers;
        }

        public async Task<bool> Update(UpdatePassengerCommand command, CancellationToken cancellationToken)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

            if (passenger == null)
                return false;

            passenger.Firstname = command.Firstname;
            passenger.Lastname = command.Lastname;
            passenger.Passport = command.Passport;
            passenger.Nationality = command.Nationality;

            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> AddTicket(int passengerId, int ticketId)
        {
            var ticket = await this.context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);
            if (ticket == null)
                return false;

            var passenger = await this.context.Passengers.FirstOrDefaultAsync(p => p.Id == passengerId);
            if (passenger == null)
                return false;

            var passengerTicket = new PassengersTickets
            {
                Passenger = passenger,
                Ticket = ticket
            };

            await this.context.AddAsync(passengerTicket);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTicket(int passengerId, int ticketId)
        {
            var passengerTicket = await this.context.PassengersTickets
                .FirstOrDefaultAsync(pt => pt.PassengerId == passengerId && pt.TicketId == ticketId);

            this.context.Remove(passengerTicket);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<PassengerDTO> SearchByPassport(string passport)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Passport == passport);

            if (passenger == null)
            {
                return default;
            }

            var passengerDTO = await passenger.ToPassengerDTO();

            return passengerDTO;
        }

        public async Task<ICollection<PassengerDTO>> SearchByFirstname(string firstname)
        {
            var passengers = await this.context.Passengers
                .Where(p => p.Firstname == firstname)
                .ToListAsync();

            var passengerDTOs = await passengers.ToPassengerDTOs();

            return passengerDTOs;
        }

        public async Task<ICollection<PassengerDTO>> SearchByLastname(string lastname)
        {
            var passengers = await this.context.Passengers
                .Where(p => p.Lastname == lastname)
                .ToListAsync();

            var passengerDTOs = await passengers.ToPassengerDTOs();

            return passengerDTOs;
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            return await this.context.Passengers.CountAsync(cancellationToken);
        }
    }
}
