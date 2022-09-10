namespace AirportProject.Application.Passengers.Commands.DeletePassenger
{
    public static class DeletePassengerCommandValidator
    {
        public static bool IsValid(this DeletePassengerCommand command)
        {
            return command.Id > 0;
        }
    }
}
