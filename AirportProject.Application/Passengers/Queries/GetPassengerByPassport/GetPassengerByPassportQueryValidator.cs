using System.Text.RegularExpressions;

namespace AirportProject.Application.Passengers.Queries.GetPassengerByPassport
{
    public static class GetPassengerByPassportQueryValidator
    {
        public static bool IsValid(this GetPassengerByPassportQuery query)
        {
            var regex = new Regex("^[a-z]{2}\\d{6}$");

            return regex.IsMatch(query.Passport);
        }
    }
}
