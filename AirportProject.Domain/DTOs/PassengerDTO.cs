using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.DTOs
{
    public class PassengerDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(8)]
        [RegularExpression("^[a-z]{2}\\d{6}$")]
        public string Passport { get; set; }
        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(6)]
        [RegularExpression("^male$|^female$")]
        public string Gender { get; set; }
    }
}
