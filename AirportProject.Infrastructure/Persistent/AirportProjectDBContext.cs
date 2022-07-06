using AirportProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace AirportProject.Infrastructure.Persistent
{
    public class AirportProjectDBContext : DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<PassengersTickets> PassengersTickets { get; set; }

        public AirportProjectDBContext(DbContextOptions<AirportProjectDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PassengersTickets>().HasKey(table => new {
                table.PassengerId,
                table.TicketId
            });
        }
    }
}
