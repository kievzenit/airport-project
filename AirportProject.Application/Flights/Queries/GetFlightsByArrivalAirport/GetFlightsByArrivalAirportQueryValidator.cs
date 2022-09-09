namespace AirportProject.Application.Flights.Queries.GetFlightsByArrivalAirport
{
    public static class GetFlightsByArrivalAirportQueryValidator
    {
        public static bool IsValid(this GetFlightsByArrivalAirportQuery query)
        {
            return query.AirportName.Length > 0 && query.AirportName.Length < 50;
        }
    }
}
