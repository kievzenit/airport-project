namespace AirportProject.Application.Common.DTOs
{
    public class AirportDTO : DTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
