using AirportProject.Application.Common.DTOs;
using MediatR;

namespace AirportProject.Application.Passengers.Queries.GetPassengerByPassport
{
    public record GetPassengerByPassportQuery(string passport) : IRequest<PassengerDTO>
    {
        public string Passport { get; init; } = passport;
    }
}
