using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.Models
{
    [Table("Passenger")]
    public class Passenger
    {
        public Passenger()
        {
            this.PassengersTickets = new List<PassengersTickets>();
        }

        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("firstname")]
        public string Firstname { get; set; }
        [Required]
        [Column("lastname")]
        public string Lastname { get; set; }
        [Required]
        [Column("passport")]
        public string Passport { get; set; }
        [Required]
        [Column("nationality")]
        public string Nationality { get; set; }
        [Required]
        [Column("birthday")]
        public DateTime Birthday { get; set; }
        [Required]
        [Column("gender")]
        public string Gender { get; set; }
        public virtual ICollection<PassengersTickets> PassengersTickets { get; set; }
    }
}
