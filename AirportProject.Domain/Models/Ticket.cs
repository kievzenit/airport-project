using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        public Ticket()
        {
            this.PassengersTickets = new List<PassengersTickets>();
        }

        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("price")]
        public decimal Price { get; set; }
        [Required]
        [Column("type")]
        public string Type { get; set; }
        [Required]
        [Column("flight_id")]
        [ForeignKey("Flight")]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual ICollection<PassengersTickets> PassengersTickets { get; set; }
    }
}
