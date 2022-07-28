using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.Models
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("arrival_airport_id")]
        [ForeignKey("ArrivalAirport")]
        public int ArrivalAirportId { get; set; }
        public virtual Airport ArrivalAirport { get; set; }
        [Required]
        [Column("departure_airport_id")]
        [ForeignKey("DepartureAirport")]
        public int DepartureAirportId { get; set; }
        public virtual Airport DepartureAirport { get; set; }
        [Required]
        [Column("terminal")]
        public char Terminal { get; set; }
        [Required]
        [Column("arrival_time")]
        public DateTime ArrivalTime { get; set; }
        [Required]
        [Column("departure_time")]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Column("status")]
        public string Status { get; set; }
    }
}
