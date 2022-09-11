namespace AirportProject.Application.Passengers.Queries.GetPassengersByLastname
{
    public static class GetPassengersByLastnameQueryValidator
    {
        public static bool IsValid(this GetPassengersByLastnameQuery query)
        {
            return query.Lastname.Length > 0 && query.Lastname.Length <= 50;
        }
    }
}
