using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Common.Casting
{
    public class PassengersCaster : ICaster<Passenger, PassengerDTO>
    {
        public Task<PassengerDTO> Cast(Passenger passenger, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PassengerDTO
            {
                Id = passenger.Id,
                Firstname = passenger.Firstname,
                Lastname = passenger.Lastname,
                Passport = passenger.Passport,
                Nationality = passenger.Nationality,
                Birthday = passenger.Birthday,
                Gender = passenger.Gender
            });
        }

        public async Task<ICollection<PassengerDTO>> Cast(
            ICollection<Passenger> passengers, CancellationToken cancellationToken)
        {
            var passengerDTOs = new List<PassengerDTO>(passengers.Count);

            foreach (var passenger in passengers)
            {
                var passengerDTO = await this.Cast(passenger, cancellationToken);

                passengerDTOs.Add(passengerDTO);
            }

            return passengerDTOs;
        }
    }
}
