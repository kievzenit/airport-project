using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain
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
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [Column("lastname")]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [Column("passport")]
        [StringLength(8)]
        [RegularExpression("^[a-z]{2}\\d{6}$")]
        public string Passport { get; set; }
        [Required]
        [Column("nationality")]
        [StringLength(50)]
        public string Nationality { get; set; }
        [Required]
        [Column("birthday")]
        public DateTime Birthday { get; set; }
        [Required]
        [Column("gender")]
        [StringLength(6)]
        [RegularExpression("^male$|^female$")]
        public string Gender { get; set; }
        public virtual ICollection<PassengersTickets> PassengersTickets { get; set; }
    }
}
