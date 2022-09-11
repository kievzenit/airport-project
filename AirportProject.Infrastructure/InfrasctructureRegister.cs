using AirportProject.Application.Common.Abstract;
using AirportProject.Infrastructure.Persistent.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AirportProject.Infrastructure
{
    public static class InfrastructureRegister
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAirportRepository, AirportRepository>();
            serviceCollection.AddScoped<IFlightRepository, FlightRepository>();
            serviceCollection.AddScoped<ITicketRepository, TicketRepository>();
            serviceCollection.AddScoped<IPassengerRepository, PassengerRepository>();
        }
    }
}
