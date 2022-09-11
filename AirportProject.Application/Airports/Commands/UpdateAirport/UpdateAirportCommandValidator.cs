using AirportProject.Application.Airports.Commands.UpdateAirport;

namespace AirportProject.Application.Airports.Command.UpdateAirport
{
    public static class UpdateAirportCommandValidator
    {

        public static bool IsValid(this UpdateAirportCommand command)
        {
            return command.Id > 0
                && IsLengthValid(command.Name)
                && IsLengthValid(command.Country)
                && IsLengthValid(command.City);
        }

        private static bool IsLengthValid(string value)
        {
            return value.Length <= 50
                && value.Length > 1;
        }
    }
}
