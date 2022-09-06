namespace AirportProject.Application.Airports.Commands.DeleteAirport
{
    public static class DeleteAirportCommandValidator
    {
        public static bool IsValid(this DeleteAirportCommand deleteAirportCommand)
        {
            return deleteAirportCommand.Id > 0;
        }
    }
}
