using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportProject.Application.Common.DTOs
{
    public class AirportDTO : DTO
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
