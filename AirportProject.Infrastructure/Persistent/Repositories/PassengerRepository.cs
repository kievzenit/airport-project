using AirportProject.Domain;
using AirportProject.DTOs;
using AirportProject.Infrastructure.Persistent.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Repositories
{
    public class PassengerRepository : AbstractRepository, IPassengerRepository
    {
        private AirportProjectDBContext dBContext;

        protected override AirportProjectDBContext context
        {
            get => this.dBContext;
            set => this.dBContext = value;
        }

        public PassengerRepository(AirportProjectDBContext context)
        {
            this.context = context;
        }

        public async Task<PassengerDTO> Create(PassengerDTO passengerDTO)
        {
            var passenger = await this.PassengerDTOToPassenger(passengerDTO);

            await this.context.AddAsync(passenger);
            await this.context.SaveChangesAsync();

            passengerDTO.Id = passenger.Id;

            return passengerDTO;
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

        public async Task<IEnumerable<PassengerDTO>> GetAll()
        {
            var passengers = await this.context.Passengers.ToListAsync();

            return await this.PassengersToPassengerDTOs(passengers);
        }

        public async Task<IEnumerable<PassengerDTO>> GetRange(int offset, int count)
        {
            var passengers = await this.context.Passengers
                .Skip((offset - 1) * count)
                .Take(count)
                .ToListAsync();

            return await this.PassengersToPassengerDTOs(passengers);
        }

        public async Task<bool> Update(PassengerDTO passengerDTO)
        {
            var passenger = await this.context.Passengers.FirstOrDefaultAsync(p => p.Id == passengerDTO.Id);

            if (passenger == null)
                return false;

            passenger.Firstname = passengerDTO.Firstname;
            passenger.Lastname = passengerDTO.Lastname;
            passenger.Passport = passengerDTO.Passport;
            passenger.Nationality = passengerDTO.Nationality;

            await this.context.SaveChangesAsync();

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
    }
}
