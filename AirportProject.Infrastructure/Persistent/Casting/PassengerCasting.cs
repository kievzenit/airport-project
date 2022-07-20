using AirportProject.Domain;
using AirportProject.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportProject.Infrastructure.Persistent.Casting
{
    public static class PassengerCasting
    {
        public static Task<Passenger> ToPassenger(this PassengerDTO passengerDTO)
        {
            return Task.FromResult(
                new Passenger
                {
                    Id = passengerDTO.Id,
                    Firstname = passengerDTO.Firstname,
                    Lastname = passengerDTO.Lastname,
                    Passport = passengerDTO.Passport,
                    Nationality = passengerDTO.Nationality,
                    Birthday = passengerDTO.Birthday,
                    Gender = passengerDTO.Gender
                });

        }

        public static Task<PassengerDTO> ToPassengerDTO(this Passenger passenger)
        {
            return Task.FromResult(
                new PassengerDTO
                {
                    Id = passenger.Id,
                    Firstname = passenger.Firstname,
                    Lastname = passenger.Lastname,
                    Passport = passenger.Passport,
                    Nationality = passenger.Nationality,
                    Birthday = passenger.Birthday,
                    Gender = passenger.Gender,
                });
        }

        public static async Task<List<PassengerDTO>> ToPassengerDTOs(this IEnumerable<Passenger> passengers)
        {
            var passengerDTOs = new List<PassengerDTO>();

            foreach (var passenger in passengers)
            {
                var passengerDTO = await passenger.ToPassengerDTO();

                passengerDTOs.Add(passengerDTO);
            }

            return passengerDTOs;
        }
    }
}
