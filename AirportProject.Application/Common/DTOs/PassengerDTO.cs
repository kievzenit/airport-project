using System;

namespace AirportProject.Application.Common.DTOs
{
    public class PassengerDTO : DTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Passport { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
    }
}
