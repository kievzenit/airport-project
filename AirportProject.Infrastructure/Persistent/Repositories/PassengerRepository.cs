using AirportProject.Application.Common.Abstract;

using AirportProject.Application.Passengers.Commands.AddTicketToPassenger;
using AirportProject.Application.Passengers.Commands.CreatePassenger;
using AirportProject.Application.Passengers.Commands.DeletePassenger;
using AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger;
using AirportProject.Application.Passengers.Commands.UpdatePassenger;

using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using AirportProject.Application.Passengers.Queries.GetPassengersByFirstname;
using AirportProject.Application.Passengers.Queries.GetPassengersByLastname;
using AirportProject.Application.Passengers.Queries.GetPassengersWithPagination;

using AirportProject.Domain.Models;

using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> Delete(DeletePassengerCommand command, CancellationToken cancellationToken)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);

            if (passenger == null)
                return false;

            var relatedPassengersTickets = await this.context.PassengersTickets
                .Where(pt => pt.Passenger == passenger)
                .ToListAsync(cancellationToken);

            this.context.RemoveRange(relatedPassengersTickets);
            this.context.Remove(passenger);
            await this.context.SaveChangesAsync(cancellationToken);

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

        public async Task<bool> AddTicket(
            AddTicketToPassengerCommand command, CancellationToken cancellationToken)
        {
            var ticket = await this.context.Tickets
                .FirstOrDefaultAsync(t => t.Id == command.TicketId, cancellationToken);
            if (ticket == null)
                return false;

            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Id == command.PassengerId, cancellationToken);
            if (passenger == null)
                return false;

            var passengerTicket = await this.context.PassengersTickets
                .FirstOrDefaultAsync(pt => pt.Passenger == passenger && pt.Ticket == ticket, cancellationToken);

            if (passengerTicket != null)
                throw new InvalidOperationException($"Passenger with id: {passenger.Id} is already have this ticket");

            passengerTicket = new PassengersTickets
            {
                Passenger = passenger,
                Ticket = ticket
            };

            await this.context.AddAsync(passengerTicket, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> DeleteTicket(
            RemoveTicketFromPassengerCommand command, CancellationToken cancellationToken)
        {
            var passengerTicket = await this.context.PassengersTickets
                .FirstOrDefaultAsync(pt => pt.PassengerId == command.PassengerId
                                        && pt.TicketId == command.TicketId,
                                        cancellationToken);

            if (passengerTicket == null)
                return false;

            this.context.Remove(passengerTicket);
            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<Passenger> SearchByPassport(
            GetPassengerByPassportQuery query, CancellationToken cancellationToken)
        {
            var passenger = await this.context.Passengers
                .FirstOrDefaultAsync(p => p.Passport == query.Passport, cancellationToken);

            return passenger;
        }

        public async Task<ICollection<Passenger>> SearchByFirstname(
            GetPassengersByFirstnameQuery query, CancellationToken cancellationToken)
        {
            var passengers = await this.context.Passengers
                .Where(p => p.Firstname == query.Firstname)
                .ToListAsync(cancellationToken);

            return passengers;
        }

        public async Task<ICollection<Passenger>> SearchByLastname(
            GetPassengersByLastnameQuery query, CancellationToken cancellationToken)
        {
            var passengers = await this.context.Passengers
                .Where(p => p.Lastname == query.Lastname)
                .ToListAsync(cancellationToken);

            return passengers;
        }

        public async Task<int> GetTotalCount(CancellationToken cancellationToken)
        {
            return await this.context.Passengers.CountAsync(cancellationToken);
        }
    }
}
