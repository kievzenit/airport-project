using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.DTOs
{
    public class FlightDTO
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Column("arrivalAirport")]
        [Required]
        public string ArrivalAirportName { get; set; }
        [Column("departureAirport")]
        [Required]
        public string DepartureAirportName { get; set; }
        [Column("terminal")]
        [Required]
        [RegularExpression("[A-Z]")]
        public char Terminal { get; set; }
        [Column("arrivalTime")]
        [Required]
        public DateTime ArrivalTime { get; set; }
        [Column("departureTime")]
        [Required]
        public DateTime DepartureTime { get; set; }
        [Column("status")]
        [Required]
        [StringLength(8)]
        [RegularExpression("^normal$|^delayed$|^canceled$")]
        public string Status { get; set; }
        [Column("economyPrice")]
        [Required]
        [Range(typeof(decimal), "100", "100000")]
        public decimal EconomyPrice { get; set; }
        [Column("businessPrice")]
        [Required]
        [Range(typeof(decimal), "100", "100000")]
        public decimal BusinessPrice { get; set; }
    }
}
