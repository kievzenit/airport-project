using AirportProject.Application.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AirportProject.Application.Passengers.Queries.GetPassengersByFirstname
{
    public record GetPassengersByFirstnameQuery(string firstname) : IRequest<IEnumerable<PassengerDTO>>
    {
        public string Firstname { get; init; } = firstname;
    }
}
