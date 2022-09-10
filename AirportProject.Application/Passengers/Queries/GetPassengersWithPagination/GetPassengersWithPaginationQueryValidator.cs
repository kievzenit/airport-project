namespace AirportProject.Application.Passengers.Queries.GetPassengersWithPagination
{
    public static class GetPassengersWithPaginationQueryValidator
    {
        public static bool IsValid(this GetPassengersWithPaginationQuery query)
        {
            return query.PageNumber > 0;
        }
    }
}
