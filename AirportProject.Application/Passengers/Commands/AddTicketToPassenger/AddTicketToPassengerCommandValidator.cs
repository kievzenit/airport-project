namespace AirportProject.Application.Passengers.Commands.AddTicketToPassenger
{
    public static class AddTicketToPassengerCommandValidator
    {
        public static bool IsValid(this AddTicketToPassengerCommand command)
        {
            return command.PassengerId > 0 && command.TicketId > 0;
        }
    }
}
