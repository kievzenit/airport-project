using AirportProject.Application.Casting;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AirportProject.Application
{
    public static class ApplicationRegister
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddScoped<FlightsCaster>();
        }
    }
}
