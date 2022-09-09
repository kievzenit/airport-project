namespace AirportProject.Application.Flights.Queries.GetFlightsByDepartureAirport
{
    public static class GetFlightsByDepartureAirportQueryValidator
    {
        public static bool IsValid(this GetFlightsByDepartureAirportQuery query)
        {
            return query.AirportName.Length > 0 && query.AirportName.Length <= 50;
        }
    }
}
