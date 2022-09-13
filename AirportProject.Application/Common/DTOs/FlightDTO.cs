using System;

namespace AirportProject.Application.Common.DTOs
{
    public class FlightDTO : DTO
    {
        public int Id { get; set; }
        public string ArrivalAirportName { get; set; }
        public string DepartureAirportName { get; set; }
        public char Terminal { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string Status { get; set; }
        public decimal EconomyPrice { get; set; }
        public decimal BusinessPrice { get; set; }
    }
}
