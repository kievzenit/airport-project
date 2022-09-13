using System;

namespace AirportProject.Application.Common.DTOs
{
    public class TicketDTO : DTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public int FlightId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
