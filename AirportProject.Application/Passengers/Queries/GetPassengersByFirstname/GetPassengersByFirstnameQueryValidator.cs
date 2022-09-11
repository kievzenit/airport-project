namespace AirportProject.Application.Passengers.Queries.GetPassengersByFirstname
{
    public static class GetPassengersByFirstnameQueryValidator
    {
        public static bool IsValid(this GetPassengersByFirstnameQuery query)
        {
            return query.Firstname.Length > 0 && query.Firstname.Length <= 50;
        }
    }
}
