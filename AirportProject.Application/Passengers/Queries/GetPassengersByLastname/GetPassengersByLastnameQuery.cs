using AirportProject.Application.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace AirportProject.Application.Passengers.Queries.GetPassengersByLastname
{
    public record GetPassengersByLastnameQuery(string lastname) : IRequest<IEnumerable<PassengerDTO>>
    {
        public string Lastname { get; init; } = lastname;
    }
}
