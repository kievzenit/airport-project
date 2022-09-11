using AirportProject.Application.Common.Abstract;
using AirportProject.Application.Common.Casting;
using AirportProject.Application.Common.Exceptions;
using AirportProject.Application.Common.DTOs;
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
        private readonly PassengersCaster caster;

        public GetPassengerByPassportQueryHandler(IPassengerRepository repository, PassengersCaster caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<PassengerDTO> Handle(
            GetPassengerByPassportQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var passenger = await this.repository.SearchByPassport(request, cancellationToken);

            if (passenger == null)
            {
                throw new NotFoundException($"Passenger with passport: {request.Passport} was not found");
            }

            var passengerDTO = await this.caster.Cast(passenger);

            return passengerDTO;
        }
    }
}
