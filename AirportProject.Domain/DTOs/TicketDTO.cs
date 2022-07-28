using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.DTOs
{
    public class TicketDTO
    {
        [Column("from")]
        [Required]
        [StringLength(50)]
        public string From { get; set; }
        [Column("to")]
        [Required]
        [StringLength(50)]
        public string To { get; set; }
        [Column("ticketType")]
        [StringLength(8)]
        [Required]
        [RegularExpression("^economy$|^business$")]
        public string Type { get; set; }
        public int Id { get; set; }
        public int FlightId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
