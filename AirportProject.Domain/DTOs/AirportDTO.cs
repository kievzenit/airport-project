using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Domain.DTOs
{
    public class AirportDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Column("country")]
        [StringLength(50)]
        [Required]
        public string Country { get; set; }
        [Column("city")]
        [StringLength(50)]
        [Required]
        public string City { get; set; }
    }
}
