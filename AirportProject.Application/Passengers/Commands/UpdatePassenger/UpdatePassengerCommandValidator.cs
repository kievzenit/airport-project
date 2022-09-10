using System.Text.RegularExpressions;

namespace AirportProject.Application.Passengers.Commands.UpdatePassenger
{
    public static class UpdatePassengerCommandValidator
    {
        public static bool IsValid(this UpdatePassengerCommand command)
        {
            return IsIdValid(command.Id)
                && IsNameLengthValid(command.Firstname.Length)
                && IsNameLengthValid(command.Lastname.Length)
                && IsPassportValid(command.Passport)
                && IsNationalityValid(command.Nationality);
        }

        private static bool IsIdValid(int id)
        {
            return id > 0;
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
    }
}
