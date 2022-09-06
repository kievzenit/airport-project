namespace AirportProject.Application.Flights.Queries.GetFlightsWithPagination
{
    public static class GetFlightsWithPaginationQueryValidator
    {
        public static bool IsValid(this GetFlightsWithPaginationQuery query)
        {
            return query.PageNumber > 0;
        }
    }
}
