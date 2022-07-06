using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.DTOs
{
    public class AirportDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        public string Name { get; set; }
        [Column("country")]
        [Required]
        public string Country { get; set; }
        [Column("city")]
        [Required]
        public string City { get; set; }
    }
}
