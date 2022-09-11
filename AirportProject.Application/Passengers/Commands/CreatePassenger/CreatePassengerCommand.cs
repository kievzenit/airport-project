using AirportProject.Application.Common.DTOs;
using MediatR;
using System;

namespace AirportProject.Application.Passengers.Commands.CreatePassenger
{
    public record CreatePassengerCommand : IRequest<PassengerDTO>
    {
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string Passport { get; init; }
        public string Nationality { get; init; }
        public DateTime Birthday { get; init; }
        public string Gender { get; init; }
    }
}
