using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Passengers.Queries.GetPassengerByPassport;
using System;
using System.Text.RegularExpressions;

namespace AirportProject.Application.Passengers.Commands.CreatePassenger
{
    public static class CreatePassengerCommandValidator
    {
        public static bool IsValid(this CreatePassengerCommand command, IPassengerRepository repository)
        {
            return IsNameLengthValid(command.Firstname.Length)
                && IsNameLengthValid(command.Lastname.Length)
                && IsPassportValid(command.Passport)
                && IsNationalityValid(command.Nationality)
                && IsBirthdayValid(command.Birthday)
                && IsGenderValid(command.Gender)
                && IsPassengerUnique(command.Passport, repository);
        }

        private static bool IsPassengerUnique(string passport, IPassengerRepository repository)
        {
            var passenger = repository.SearchByPassport(
                new GetPassengerByPassportQuery(passport),
                default).Result;

            return passenger == null;
        }

        private static bool IsNameLengthValid(int length)
        {
            return length > 0 && length <= 50;
        }

        private static bool IsPassportValid(string passport)
        {
            var regex = new Regex("^[a-z]{2}\\d{6}$");

            return regex.IsMatch(passport);
        }

        private static bool IsNationalityValid(string nationality)
        {
            return nationality.Length > 0 && nationality.Length <= 50;
        }

        private static bool IsBirthdayValid(DateTime birthday)
        {
            return DateTime.Now > birthday;
        }

        private static bool IsGenderValid(string gender)
        {
            var regex = new Regex("^male$|^female$");

            return regex.IsMatch(gender);
        }
    }
}
