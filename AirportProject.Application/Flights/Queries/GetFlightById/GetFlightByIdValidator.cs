namespace AirportProject.Application.Flights.Queries.GetFlightById
{
    public static class GetFlightByIdValidator
    {
        public static bool IsValid(this GetFlightByIdQuery query)
        {
            return query.Id > 0;
        }
    }
}
