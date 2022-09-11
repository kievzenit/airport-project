using System.Text.RegularExpressions;

namespace AirportProject.Application.Tickets.Queries.GetSpecificTickets
{
    public static class GetSpecificTicketsQueryValidator
    {
        public static bool IsValid(this GetSpecificTicketsQuery query)
        {
            return IsFromAndToValid(query.From, query.To)
                && IsTypeValid(query.Type);
        }

        private static bool IsFromAndToValid(string from, string to)
        {
            return from.Length > 0 && from.Length <= 50
                && to.Length > 0 && to.Length <= 50;
        }

        private static bool IsTypeValid(string type)
        {
            var regex = new Regex("^economy$|^business$");

            return regex.IsMatch(type);
        }
    }
}
