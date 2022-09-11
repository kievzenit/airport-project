using AirportProject.Application.Abstract;
using AirportProject.Application.Casting;
using AirportProject.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirportProject.Application.Passengers.Queries.GetPassengersByFirstname
{
    public class GetPassengersByFirstnameQueryHandler :
        IRequestHandler<GetPassengersByFirstnameQuery, IEnumerable<PassengerDTO>>
    {
        private readonly IPassengerRepository repository;
        private readonly PassengersCaster caster;

        public GetPassengersByFirstnameQueryHandler(IPassengerRepository repository, PassengersCaster caster)
        {
            this.repository = repository;
            this.caster = caster;
        }

        public async Task<IEnumerable<PassengerDTO>> Handle(
            GetPassengersByFirstnameQuery request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                throw new ArgumentException("Input data was not in correct format");
            }

            var passengers = await this.repository.SearchByFirstname(request, cancellationToken);

            return await this.caster.Cast(passengers);
        }
    }
}
