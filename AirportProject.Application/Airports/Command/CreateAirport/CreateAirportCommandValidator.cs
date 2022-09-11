using AirportProject.Application.Common.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Airports.Commands.CreateAirport
{
    public static class CreateAirportCommandValidator
    {

        public static async Task<bool> IsValid(
            this CreateAirportCommand command, 
            IAirportRepository repository,
            CancellationToken cancellationToken)
        {
            return await IsNameValid(command.Name, repository, cancellationToken)
                && IsCountryValid(command.Country)
                && IsCityValid(command.City);
        }

        private static async Task<bool> IsNameValid(
            string name, 
            IAirportRepository repository,
            CancellationToken cancellationToken)
        {
            return IsLengthValid(name)
                && await IsUniqueName(name, repository, cancellationToken);
        }

        private static bool IsCountryValid(string country)
        {
            return IsLengthValid(country);
        }

        private static bool IsCityValid(string city)
        {
            return IsLengthValid(city);
        }

        private static bool IsLengthValid(string value)
        {
            return value.Length <= 50
                && value.Length > 1;
        }

        private static async Task<bool> IsUniqueName(
            string name,
            IAirportRepository repository,
            CancellationToken cancellationToken)
        {
            return !await repository.DoesAirportExists(name, cancellationToken);
        }
    }
}
