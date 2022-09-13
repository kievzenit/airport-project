using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Casting;
using AirportProject.Application.Common.DTOs;
using AirportProject.Domain.Models;
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

            serviceCollection.AddScoped<ICaster<Airport, AirportDTO>, AirportsCaster>();
            serviceCollection.AddScoped<ICaster<Flight, FlightDTO>, FlightsCaster>();
            serviceCollection.AddScoped<ICaster<Passenger, PassengerDTO>, PassengersCaster>();
            serviceCollection.AddScoped<ICaster<Ticket, TicketDTO>, TicketsCaster>();
        }
    }
}
