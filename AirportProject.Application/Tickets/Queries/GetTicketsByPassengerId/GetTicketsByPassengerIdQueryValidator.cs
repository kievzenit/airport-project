namespace AirportProject.Application.Tickets.Queries.GetTicketsByPassengerId
{
    public static class GetTicketsByPassengerIdQueryValidator
    {
        public static bool IsValid(this GetTicketsByPassengerIdQuery query)
        {
            return query.PassengerId > 0;
        }
    }
}
