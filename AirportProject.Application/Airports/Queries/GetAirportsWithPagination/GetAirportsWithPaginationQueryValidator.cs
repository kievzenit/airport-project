namespace AirportProject.Application.Airports.Queries.GetAirportsWithPagination
{
    public static class GetAirportsWithPaginationQueryValidator
    {
        public static bool IsValid(this GetAirportsWithPaginationQuery query)
        {
            return query.PageNumber > 0;
        }
    }
}
