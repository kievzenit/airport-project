using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.Models
{
    [Table("PassengersTickets")]
    public class PassengersTickets
    {
        [Key, Column("passenger_id", Order = 1)]
        [Required]
        [ForeignKey("Passenger")]
        public int PassengerId { get; set; }
        public virtual Passenger Passenger { get; set; }
        [Key, Column("ticket_id", Order = 2)]
        [Required]
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
