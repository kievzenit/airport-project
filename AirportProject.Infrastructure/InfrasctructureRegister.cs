using AirportProject.Application.Abstract;
using AirportProject.Infrastructure.Persistent.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AirportProject.Infrastructure
{
    public static class InfrastructureRegister
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAirportRepository, AirportRepository>();
            serviceCollection.AddTransient<IFlightRepository, FlightRepository>();
            serviceCollection.AddTransient<ITicketRepository, TicketRepository>();
            serviceCollection.AddTransient<IPassengerRepository, PassengerRepository>();
        }
    }
}
