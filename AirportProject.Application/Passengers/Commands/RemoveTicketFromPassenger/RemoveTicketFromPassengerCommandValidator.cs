namespace AirportProject.Application.Passengers.Commands.RemoveTicketFromPassenger
{
    public static class RemoveTicketFromPassengerCommandValidator
    {
        public static bool IsValid(this RemoveTicketFromPassengerCommand command)
        {
            return command.PassengerId > 0 && command.TicketId > 0;
        }
    }
}
