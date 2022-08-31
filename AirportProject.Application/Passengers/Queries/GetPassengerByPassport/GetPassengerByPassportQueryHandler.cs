using AirportProject.Application.Abstract;
using AirportProject.Application.Exceptions;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Queries.GetPassengerByPassport
{
    public class GetPassengerByPassportQueryHandler :
        IRequestHandler<GetPassengerByPassportQuery, PassengerDTO>
    {
        private readonly IPassengerRepository repository;

        public GetPassengerByPassportQueryHandler(IPassengerRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PassengerDTO> Handle(
            GetPassengerByPassportQuery request, CancellationToken cancellationToken)
        {
            if (request.Passport == null
                || request.Passport.Length != 8
                || !Regex.IsMatch(request.Passport, "^[a-z]{2}\\d{6}$"))
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var passengerDTO = await this.repository.SearchByPassport(request.Passport);

            if (passengerDTO == null)
            {
                throw new NotFoundException($"Passenger with passport: {request.Passport} was not found");
            }

            return passengerDTO;
        }
    }
}
