namespace AirportProject.Application.Flights.Commands.DeleteFlight
{
    public static class DeleteFlightCommandValidator
    {
        public static bool IsValid(this DeleteFlightCommand command)
        {
            return command.Id > 0;
        }
    }
}
